import React from "react";

interface TicketInfoProps {
  title: string;
  date: string;
  cinema: string;
  ticketsBought: number;
  bookingId: number;
}

const TicketInfo: React.FC<TicketInfoProps> = ({ title, date, cinema, ticketsBought, bookingId }) => {
  return (
    <div
      className="ticket-card"
      style={{
        width: "50%",
        border: "1px solid #ccc",
        borderRadius: "10px",
        padding: "25px",
        marginBottom: "15px",
        boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
      }}
    >
      <h2 className="" style={{ margin: "0 0 10px 0", fontSize: "2em" }}>{title}</h2>
      <p>
        <strong>Date:</strong> {date}
      </p>
      <p>
        <strong>Cinema:</strong> {cinema}
      </p>
      <p>
        <strong>Tickets Bought:</strong> {ticketsBought}
      </p>
      <p>
        <strong>Booking ID:</strong> {bookingId}
      </p>
    </div>
  );
};

export default TicketInfo;
