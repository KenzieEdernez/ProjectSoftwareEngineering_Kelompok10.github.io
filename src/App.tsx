import { Register } from "./pages/Register";
import { Login } from "./pages/Login";
import { NowPlaying } from "./pages/NowPlaying";
import { UpComing } from "./pages/UpComing";
import { Theaters } from "./pages/Theaters";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<NowPlaying />} />
        <Route path="upcoming" element={<UpComing />} />
        <Route path="theaters" element={<Theaters />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
