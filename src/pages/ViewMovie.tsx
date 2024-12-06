
import { useEffect } from "react";
import {NavbarAdmin} from "../components/NavbarAdmin";
import MovieInfo from "../components/MovieInfo";
import axios from "axios";
function ViewMovie(){

    const movies = [
        {
            scheduleId: "S001",
            movieId: "M001",
            title: "Inception",
            theaterId: "T001",
            theaterName: "Cineplex 21",
            showtime: "18:30",
            ticketPrice: "50000"
        },
        {
            scheduleId: "S002",
            movieId: "M002",
            title: "The Matrix",
            theaterId: "T002",
            theaterName: "XXI Plaza",
            showtime: "20:00",
            ticketPrice: "60000"
        },
        {
            scheduleId: "S003",
            movieId: "M003",
            title: "The Substance",
            theaterId: "T002",
            theaterName: "Plaza Indonesia",
            showtime: "20:00",
            ticketPrice: "65000"
        },
        {
            scheduleId: "S004",
            movieId: "M003",
            title: "The Substance",
            theaterId: "T002",
            theaterName: "Central Park",
            showtime: "17:30",
            ticketPrice: "65000"
        },
        {
            scheduleId: "S005",
            movieId: "M003",
            title: "Moana 2",
            theaterId: "T003",
            theaterName: "Central Park",
            showtime: "17:30",
            ticketPrice: "65000"
        }
    ];
      useEffect( () => 
        {
            document.body.style.backgroundColor = '#f3f4f6'
            
            return () => {
               
                document.body.style.backgroundColor = ''
            };
        }
    , [] );
    return(
        <>
            <NavbarAdmin/>
            <div className="view-container">
                <div className="view-header">
                    <span>Schedule ID</span>
                    <span>Movie ID</span>
                    <span>Movie Title</span>
                    <span>Theater ID</span>
                    <span>Theater Name</span>
                    <span>Showtime</span>
                    <span>Ticket Price</span>
                </div>
                {movies.map((movie, index) => (

                        <MovieInfo
                            key={index}
                            scheduleId={movie.scheduleId}
                            movieId={movie.movieId}
                            title={movie.title}
                            theaterId={movie.theaterId}
                            theaterName={movie.theaterName}
                            showtime={movie.showtime}
                            ticketPrice={movie.ticketPrice}
                        
                        />
     
                    ))}


           
            </div>

        </>

    )
}
export default ViewMovie