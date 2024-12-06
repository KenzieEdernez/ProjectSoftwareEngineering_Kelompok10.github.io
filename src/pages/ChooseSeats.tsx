import React, { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const ChooseSeats: React.FC = () => {
  const navigate = useNavigate();
  const rows = 7;
  const columns = 9;
  const seatPrice = 50000;
  const { title } = useParams<{ title: string }>();
  const location = useLocation();
  const { imagePath, theaterName, time } = location.state || {};

  const [selectedSeats, setSelectedSeats] = useState<string[]>([]);
  const [totalPrice, setTotalPrice] = useState(0);

  const handleSeatClick = (seatLabel: string) => {
    setSelectedSeats((prevSelectedSeats) => {
      if (prevSelectedSeats.includes(seatLabel)) {
        let curPrice = totalPrice;
        setTotalPrice(curPrice - seatPrice);
        // Remove seat if already selected
        return prevSelectedSeats.filter((seat) => seat !== seatLabel);
      } else {
        let curPrice = totalPrice;
        setTotalPrice(curPrice + seatPrice);
        // Add seat if not selected
        return [...prevSelectedSeats, seatLabel];
      }
    });
  };

  const renderSeats = (isLeft: boolean) => {
    const seats = [];
    for (let row = 0; row < rows; row++) {
      for (let col = 0; col < columns; col++) {
        const seatNumber = isLeft ? col + 1 : col + 10;
        const seatLabel = `${String.fromCharCode(65 + row)}${seatNumber}`;
        const isSelected = selectedSeats.includes(seatLabel);
        seats.push(
          <div
            key={`${row}-${col}`}
            className={`w-8 h-8 m-1 rounded flex items-center justify-center cursor-pointer text-white ${
              isSelected ? "bg-yellow-400 text-blue-950 " : "bg-gray-800"
            }`}
            onClick={() => handleSeatClick(seatLabel)}
          >
            {seatLabel}
          </div>
        );
      }
    }
    return seats;
  };

  const handleOrderDetail = () => {
    if (selectedSeats.length > 0) {
      navigate(`/orderdetail/${title}`, {
        state: {
          imagePath,
          movieTitle: title,
          theaterName,
          time,
          selectedSeats,
          totalPrice,
        },
      });
    }
  };

  // useEffect(() => {
  //   if (!theaterName || !time) {
  //     navigate('/');
  //   }
  // }, [theaterName, time, navigate]);

  const handleBackClick = () => {
    navigate(`/moviedesc/${title}`, {
      state: {
        imagePath,
        theaterName,
        time,
      },
    });
  };

  return (
    <div className="bg-darkBlue h-screen p-5">
      <header className="mb-10">
        <button
          onClick={handleBackClick}
          className="bg-yellow-400 rounded px-3 py-2 flex items-center gap-x-2 my-4 font-semibold"
        >
          <i className="fa-solid fa-arrow-left text-blue-950"></i>
          Back
        </button>
        <div className="text-yellow-400  font-bold text-2xl">{theaterName}</div>
        <p className="text-yellow-400">{time}</p>
      </header>

      <div className="flex flex-col justify-evenly lg:flex-row">
        <div className="flex flex-col ">
          <div className="flex justify-between">
            <div className="flex flex-col items-center">
              {/* seats container */}
              <div className="grid grid-cols-9 gap-1">
                {/* seats kiri */}
                {renderSeats(true)}
              </div>
            </div>

            <div className="flex flex-col items-center ml-8">
              {/* seats container */}
              <div className="grid grid-cols-9 gap-1">
                {/* seats kanan */}
                {renderSeats(false)}
              </div>
            </div>
          </div>

          {/* theater screen */}
          <div className="w-full h-15 mt-4 text-center rounded-t-full border-t-2 border-yellow-500 text-white pt-5">
            Screen
          </div>
        </div>

        {/* booking info container */}
        <div className="mt-4 lg:mt-0 lg:ml-8 text-white bg-gray-800 bg-opacity-60 p-4 rounded-lg shadow-lg w-full lg:w-1/4 flex flex-col justify-between">
          <div>
            <p className="font-semibold text-lg mb-2">Your booking info: </p>
            <div>
              <p>Seats:</p>
              <div className="flex flex-wrap mb-4">
                {selectedSeats.map((seat) => (
                  <div
                    key={seat}
                    className="m-1 p-1 bg-yellow-400 rounded text-blue-950"
                  >
                    {seat}
                  </div>
                ))}
              </div>
            </div>
          </div>

          <div className="flex justify-between items-center">
            <p>Total Price: Rp {totalPrice}</p>
            <button
              className="rounded text-blue-950 bg-yellow-400 px-3 py-1"
              onClick={handleOrderDetail}
            >
              Order Detail
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ChooseSeats;
