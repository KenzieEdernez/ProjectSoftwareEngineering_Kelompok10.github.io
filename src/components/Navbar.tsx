import React, { useState } from 'react';
import { NavbarGuest } from './NavbarGuest';
import { NavbarUser } from './NavbarUser';

interface NavbarProps {
  onLoginClick: () => void;
  onRegisterClick: () => void;
}

export const Navbar: React.FC<NavbarProps> = ({ onLoginClick, onRegisterClick }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('JohnDoe');
  return (
    <header>
      {isLoggedIn ? <NavbarUser username={username}/> : <NavbarGuest onLoginClick={onLoginClick} onRegisterClick={onRegisterClick}/>}

    </header>
  );
};

export default Navbar;