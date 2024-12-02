using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketStoreAPI.Data;
using TicketStoreAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TicketStoreAPI.Models.request;
using System.Security.Cryptography;

namespace TicketStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TicketStoreAPIContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TicketStoreAPIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser != null)
            {
                return BadRequest("Email is already taken");
            }

            var hashedPassword = HashPassword(model.Password);

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = hashedPassword,
                Phone = model.Phone
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(newUser);

            return Ok(new
            {
                UserId = newUser.UsersId,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Token = token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password not found");
            }

            if (!VerifyPassword(model.Password, user.Password))
            {
                return Unauthorized("Invalid email or password wrong pass");
            }

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                UserId = user.UsersId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            });
        }

        private string HashPassword(string password)
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            var saltAndHash = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, saltAndHash, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, saltAndHash, salt.Length, hash.Length);

            return Convert.ToBase64String(saltAndHash);
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var storedHashBytes = Convert.FromBase64String(storedPassword);
            var salt = new byte[16];
            Buffer.BlockCopy(storedHashBytes, 0, salt, 0, 16);
            var originalHash = new byte[storedHashBytes.Length - 16];
            Buffer.BlockCopy(storedHashBytes, 16, originalHash, 0, originalHash.Length);
            var enteredPasswordHash = KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return CryptographicOperations.FixedTimeEquals(originalHash, enteredPasswordHash);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, $"{user.UsersId}"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
