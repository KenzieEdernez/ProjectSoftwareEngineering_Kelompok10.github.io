import React, { useState } from 'react';
import { NavbarGuest } from './NavbarGuest';
import { NavbarUser } from './NavbarUser';

export const Navbar = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('JohnDoe');
  
  return (
    <header>
      {isLoggedIn ? <NavbarUser username={username}/> : <NavbarGuest/>}

    </header>
  );
};

export default Navbar;