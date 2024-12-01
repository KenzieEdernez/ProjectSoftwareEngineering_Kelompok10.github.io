import { useState } from "react";
import { Navbar } from "./components/Navbar";
import { Register } from "./pages/Register";
import "./App.css";
import { Login } from "./pages/Login";

const App = () => {
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);

  const handleLoginClick = () => {
    setShowRegister(false);
    setShowLogin(true);
  };

  const handleRegisterClick = () => {
    setShowLogin(false);
    setShowRegister(true);
  }

  return (
    <>
      <Navbar onLoginClick={handleLoginClick} onRegisterClick={handleRegisterClick}/>
      {showLogin && <Login />}
      {showRegister && <Register />}
    </>
  );
};

export default App;
