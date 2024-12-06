import React, { useState } from "react";
import Navbar from "../components/Navbar";
import TicketInfo from "../components/TicketInfo"; // Import the TicketInfo component

interface Ticket {
  id: number;
  title: string;
  date: string;
  cinema: string;
  ticketsBought: number;
  bookingId: number;
}

function MyTicket() {
  const [tickets] = useState<Ticket[]>([
    {
      id: 1,
      title: "Avengers: Endgame",
      date: "2024-12-15",
      cinema: "Cinema XXI - Mall A",
      ticketsBought: 3,
      bookingId: 123,
    },
    {
      id: 2,
      title: "Frozen 2",
      date: "2024-12-20",
      cinema: "Cinema XXI - Mall B",
      ticketsBought: 2,
      bookingId: 356,
    },
  ]);

  return (
    <>
      <Navbar />
      <div className="myticket-container m-10" style={{ padding: "20px" }}>
        <h1 className="" style={{ fontSize: "3em", opacity: "1" }}>Order History</h1>
        <hr className="mb-5" style={{ opacity: "1", color: "black", height: "20px" }} />
        <div className="ticket-list">
          {tickets.map((ticket) => (
            <TicketInfo
              key={ticket.id}
              title={ticket.title}
              date={ticket.date}
              cinema={ticket.cinema}
              ticketsBought={ticket.ticketsBought}
              bookingId={ticket.bookingId}
            />
          ))}
        </div>
      </div>
    </>
  );
}

export default MyTicket;
