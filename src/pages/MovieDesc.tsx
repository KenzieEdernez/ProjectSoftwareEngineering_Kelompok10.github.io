import React, { useState } from "react";
import { useParams, useLocation } from "react-router-dom";
import Navbar from "../components/Navbar";

interface MovieDataProps {
  duration: number;
  synopsis: string;
  rating: string;
  theaters: { name: string; showtimes: string[] }[];
}

const movieData: Record<string, MovieDataProps> = {
  "Movie 1": {
    duration: 101,
    synopsis: "Lorem ipsum dolor sit amet, consectetur adipisicing elit...",
    rating: "⭐⭐⭐⭐⭐",
    theaters: [
      { name: "Cinema A", showtimes: ["10:00", "13:00", "16:00"] },
      { name: "Cinema B", showtimes: ["11:00", "14:00", "17:00"] },
    ],
  },
  "Movie 2": {
    duration: 120,
    synopsis: "Lorem ipsum dolor sit amet, consectetur adipisicing elit...",
    rating: "⭐⭐⭐⭐",
    theaters: [
      { name: "Cinema C", showtimes: ["12:00", "15:00 ", "18:00"] },
      { name: "Cinema D", showtimes: ["13:00", "16:00", "19:00"] },
    ],
  },
  "Movie 3": {
    duration: 95,
    synopsis: "Lorem ipsum dolor sit amet, consectetur adipisicing elit...",
    rating: "⭐⭐⭐⭐⭐",
    theaters: [
      { name: "Cinema E", showtimes: ["10:30", "11:30", "12:30"] },
    ],
  },
  "Movie 4": {
    duration: 125,
    synopsis: "Lorem ipsum dolor sit amet, consectetur adipisicing elit...",
    rating: "⭐⭐⭐⭐⭐",
    theaters: [
      { name: "Cinema E", showtimes: ["10:30", "11:30", "12:30"] },
    ],
  },
};

const MovieDesc: React.FC = () => {
  const { title } = useParams<{ title: string }>();
  const location = useLocation();
  const { imagePath } = location.state || {};

  const movie = movieData[title || ""];

  
  const [selectedTheater, setSelectedTheater] = useState<string | null>(null);
  const [selectedShowtime, setSelectedShowtime] = useState<string | null>(null);

  const handleShowtimeClick = (theaterName: string, showtime: string) => {
    setSelectedTheater(theaterName);
    setSelectedShowtime(showtime);
  };

  return (
    <>
      <Navbar />
      <div className="p-10 movie-desc-container">
        {imagePath && (
          <img
            src={imagePath}
            alt={title}
            className="movie-desc-image"
          />
        )}
        <p className="movie-desc-inner">
          <h3><strong>{decodeURIComponent(title || "")}</strong></h3>
          <strong className="ml-0">Genre</strong>
          <br /><br />
          <p className="movie-desc-synopsis">{movie.synopsis}</p>
          <br /><br /><br />

          {movie && (
            <>
              <div style={{ display: 'flex', gap: '100px', opacity: '0.7' }}>
                <b>Duration: {movie.duration} Minutes</b>
                <b>Rating: {movie.rating}</b>
              </div>
            </>
          )}
        </p>
      </div>

      <div className="theatre-list-container p-9">
        <h2 style={{ fontWeight: 'bold', fontSize: '1.5em' }} className="">Playing At</h2>
        <div className="theatres">
          {movie?.theaters?.length > 0 ? (
            movie.theaters.map((theater, index) => (
              <div key={index} className="theatre">
                <h4>{theater.name}</h4>
                <ul>
                  {theater.showtimes.map((time, idx) => (
                   
                      <button onFocus={(e)=>(e.target.style.backgroundColor = 'hsla(220, 97%, 56%, 0.798)')} onBlur={(e)=>(e.target.style.backgroundColor = '')}
                        onClick={() => handleShowtimeClick(theater.name, time)}>
                        {time}
                      </button>
                
                  ))}
                </ul>
              </div>
            ))
          ) : (
            <p>No theaters available for this movie.</p>
          )}
        </div>
        <div className="selected-info">
          <h3 style={{fontWeight
            : 'bold', fontSize: '1.2em'}}>Selected Showtime</h3>
          <p><strong>Theater:</strong> {selectedTheater}</p>
          <p><strong>Time:</strong> {selectedShowtime}</p>
            <button className="buy-ticket">Buy</button>
        </div>
      </div>

     
      
      
    </>
  );
};

export default MovieDesc;
