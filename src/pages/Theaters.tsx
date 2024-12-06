import React from "react";
import Navbar from "../components/Navbar";
import MallCard from "../components/MallCard";
// import CentralParkImage from "../assets/gambarMall/Central-Park-Mall-exterior-scaled.jpg";
// import TamanAnggrekImage from "../assets/gambarMall/taman-anggrek.jpg";



const malls = [
  { mallName: "Central Park", location: "Jalan X" },
  {  mallName: "Taman Anggrek", location: "Jalan Y" },
  { mallName: "Central Park", location: "Jalan X" },
  { mallName: "Taman Anggrek", location: "Jalan Y" },
  {mallName: "Central Park", location: "Jalan X" },
  {  mallName: "Taman Anggrek", location: "Jalan Y" },
];


export const Theaters = () => {
  return (
    <>
      <Navbar onLoginClick={() => {}} onRegisterClick={() => {}} />
    
    <div className="mall-card-container">
      {malls.map( (mall, i) =>(
          <MallCard key={i} mallName={mall.mallName} location={mall.location}/>
      ))
      }
    </div>
     


      
    </>
  );
};
