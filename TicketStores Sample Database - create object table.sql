-- Use DataBase
USE TicketStores;
GO

-- Create Table
CREATE TABLE movies (
	movies_id INT PRIMARY KEY IDENTITY(1,1),
	title VARCHAR(255) NOT NULL,
	genre VARCHAR(100),
	duration INT NOT NULL,
	rating DECIMAL (3,1),
	description TEXT,
	poster_url VARCHAR(255),
	release_date DATE
);

CREATE TABLE theaters (
	theaters_id INT PRIMARY KEY IDENTITY(1,1),
	theaters_name VARCHAR(100) NOT NULL,
	capacity INT NOT NULL
);

CREATE TABLE schedules (
	schedules_id INT PRIMARY KEY IDENTITY(1,1),
	movie_id INT NOT NULL,
	theater_id INT NOT NULL,
	show_time DATETIME NOT NULL,
	price DECIMAL(10,2) NOT NULL,
	FOREIGN KEY (movie_id) REFERENCES movies(movies_id),
	FOREIGN KEY (theater_id) REFERENCES theaters(theaters_id)
);

CREATE TABLE seats (
	seats_id INT PRIMARY KEY IDENTITY(1,1),
	theater_id INT NOT NULL,
	seat_number VARCHAR(10) NOT NULL,
	FOREIGN KEY (theater_id) REFERENCES theaters(theaters_id)
);

CREATE TABLE users (
	users_id INT PRIMARY KEY IDENTITY(1,1),
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL,
	passwords VARCHAR(255) NOT NULL,
	phone VARCHAR(15) NOT NULL
);

CREATE TABLE bookings (
	bookings_id INT PRIMARY KEY IDENTITY(1,1),
	schedule_id INT NOT NULL,
	users_id INT NOT NULL,
	total_price DECIMAL(10,2) NOT NULL,
	bookings_status tinyint NOT NULL,
	-- Bookings status: 1 = Pending; 2 = Processing; 3 = Rejected; 4 = Completed
	created_at DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (schedule_id) REFERENCES schedules(schedules_id),
	FOREIGN KEY (users_id) REFERENCES users(users_id)
);

CREATE TABLE booking_seats (
	booking_seats_id INT PRIMARY KEY IDENTITY(1,1),
	booking_id INT NOT NULL,
	seat_id INT NOT NULL,
	FOREIGN KEY (booking_id) REFERENCES bookings(bookings_id)
);