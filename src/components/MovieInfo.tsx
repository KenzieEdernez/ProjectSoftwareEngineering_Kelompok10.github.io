import React from "react";
interface MovieInfoProps {
    scheduleId: string;
    movieId: string;
    title: string;
    theaterId: string;
    theaterName: string;
    showtime: string;
    ticketPrice: string;
}
function MovieInfo({ scheduleId, movieId, title, theaterId, theaterName, showtime, ticketPrice }: MovieInfoProps) {
    return (
        <div className="movie-info">
            <span className="movie-info-inner">{scheduleId}</span>
            <span className="movie-info-inner">{movieId}</span>
            <span className="movie-info-inner">{title}</span>
            <span className="movie-info-inner">{theaterId}</span>
            <span className="movie-info-inner">{theaterName}</span>
            <span className="movie-info-inner">{showtime}</span>
            <span className="movie-info-inner">{ticketPrice}</span>
        </div>
    );
}
export default MovieInfo