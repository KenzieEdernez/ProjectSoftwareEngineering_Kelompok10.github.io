import { useNavigate } from "react-router-dom";

export const NavbarAdmin = () => {
  const navigate = useNavigate();

  return (
    <nav className="bg-gray-800 p-4">
      <ul className="flex items-center justify-between mx-20">
        <div className="flex space-x-14">
          <li className="text-white text-lg font-bold">
            <a href="/">QUICK TIX</a>
          </li>
          <div className="flex space-x-4">
            <li>
              <a onClick={() => navigate("/admin/addmovie")} href="" className="text-gray-300 hover:text-white px-3 py-2 rounded-md text-sm font-medium">Add Movie</a>
            </li>
            <li>
              <a onClick={() => navigate('/admin/editmovie')} href="" className="text-gray-300 hover:text-white px-3 py-2 rounded-md text-sm font-medium">Edit Movie</a>
            </li>
            <li>
              <a onClick={() => {navigate('/admin/deletemovie')}} href="" className="text-gray-300 hover:text-white px-3 py-2 rounded-md text-sm font-medium">Delete Movie  </a>
            </li>
            <li>
              <a onClick={() => {navigate('/admin/viewmovie')}} href="" className="text-gray-300 hover:text-white px-3 py-2 rounded-md text-sm font-medium">View Movie  </a>
            </li>
          </div>                       
        </div>
        <div className="flex space-x-20">
          
          <div className="flex space-x-4">
            <li>
              <p className="bg-gray-800 text-white font-bold py-2 px-4 rounded">As Admin</p>
            </li>
          
          </div>
        </div>
      </ul>
    </nav>
  )
}