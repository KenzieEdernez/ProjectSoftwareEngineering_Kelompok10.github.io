import React from "react";
import { useLocation, useNavigate } from "react-router-dom";

export const OrderDetail: React.FC = () => {
  const navigate = useNavigate();
  const serviceFee = 4000;
  const location = useLocation();
  const { imagePath, movieTitle, theaterName, time, selectedSeats, totalPrice } =
    location.state || {}

    const handleBackClick = () => {
      navigate(`/chooseseats/${movieTitle}`, {
        state: {
          imagePath,
          movieTitle,
          theaterName,
          time,
          selectedSeats,
          totalPrice
        },
      });
    };

  return (
    <div className="bg-darkBlue h-screen p-5">
      <button
        className="bg-yellow-400 rounded px-3 py-2 flex items-center gap-x-2 my-4 font-semibold"
        onClick={handleBackClick}
      >
        <i className="fa-solid fa-arrow-left text-blue-950"></i>
        Back
      </button>
      <div className="flex items-center justify-center  text-white ">
        <div className="flex flex-col bg-gray-800 bg-opacity-60 p-20">
          <h1 className="text-center font-bold text-2xl mb-6">Order Detail</h1>
          <div className="flex gap-x-28 mb-6">
            <img src={imagePath} alt="movie_poster" className="w-40" />
            <div className="flex flex-col">
              <p className="font-semibold text-lg mb-2">{movieTitle}</p>
              <p>{theaterName}</p>
              <p>{time}</p>
            </div>
          </div>

          <div className="flex flex-col">
            <div className="flex justify-between mb-3">
              <span>Tickets: </span>
              <span>{selectedSeats.join(", ")}</span>
            </div>

            <div className="flex justify-between mb-3">
              <span>Price: </span>
              <span>{totalPrice}</span>
            </div>

            <div className="flex justify-between mb-16">
              <span>Service fee:</span>
              <span>Rp 4000</span>
            </div>
          </div>

          <div className="flex items-center justify-between">
            <div>
              <p className="font-semibold">Total Payment:</p>
              {totalPrice+serviceFee}
            </div>
            <button className="bg-yellow-400 text-blue-950 px-4 py-2 rounded font-semibold">
              Pay
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
