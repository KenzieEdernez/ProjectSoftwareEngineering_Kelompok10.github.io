import { Register } from "./pages/Register";
import { Login } from "./pages/Login";
import { NowPlaying } from "./pages/NowPlaying";
import { UpComing } from "./pages/UpComing";
import { Theaters } from "./pages/Theaters";
import AddMovie  from "./pages/AddMovie";
import EditMovie  from "./pages/EditMovie";
import DeleteMovie  from "./pages/DeleteMovie";
import ViewMovie  from "./pages/ViewMovie";
import MovieDesc  from "./pages/MovieDesc";

import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import ChooseSeats from "./pages/ChooseSeats";
import ChooseSeats2 from "./pages/ChooseSeats2";
import MyTicket from "./pages/MyTicket";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<NowPlaying />} />
        <Route path="upcoming" element={<UpComing />} />
        <Route path="theaters" element={<Theaters />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />

        <Route path="/moviedesc/:title" element={<MovieDesc />} />
        <Route path="/admin/" element={<AddMovie />} />
        <Route path="/admin/editmovie" element={<EditMovie />} />
        <Route path="/admin/deletemovie" element={<DeleteMovie />} />
        <Route path="/admin/viewmovie" element={<ViewMovie />} />

        <Route path="/seats" element={<ChooseSeats/>}/>
        <Route path="/seats2" element={<ChooseSeats2/>}/>
        <Route path="/myticket" element={<MyTicket/>}/>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
