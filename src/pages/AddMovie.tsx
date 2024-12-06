
import { useEffect } from "react";
import {NavbarAdmin} from "../components/NavbarAdmin";



function AddMovie(){

    const handleAddPoster = () => {
        const inputElement = document.getElementById("moviePoster");
        if (inputElement) {
          (inputElement as HTMLInputElement).click(); 
        }
      };
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
            <div className="addmovie-container">

                <div className="addmovie-container-inner">
                    <label htmlFor="">Movie ID</label>
                    <input type="text" className="AddMovie-input"/>
                </div>
                
                <div className="addmovie-container-inner">
                    <label htmlFor="">Schedule ID</label>
                    <input type="text" className="AddMovie-input" />
                </div>

                <div className="addmovie-container-inner">
                    <label htmlFor="">Theater ID</label>
                    <input type="text" className="AddMovie-input"/>
                </div>

                <div className="addmovie-container-inner">
                    <label htmlFor="">Showtime</label>
                    <input type="text" className="AddMovie-input"/>
                </div>

                <div className="addmovie-container-inner">
                    <label htmlFor="">Movie Title</label>
                    <input type="text" className="AddMovie-input" />
                </div>

                <div className="addmovie-container-inner2">
                    <div className="addmovie-container-inner3">
                        <label htmlFor="">Ticket Price</label>
                        <input type="text" className="AddMovie-input"/>
                    </div>
                    <div className="addmovie-container-inner3">
                        <label htmlFor="">Movie Poster</label>
                        <button onClick={handleAddPoster} className="add-images-btn">Add Images</button>
                        <input type="file" id="moviePoster"  style={{ display: "none" }} /> 
                    </div>
                </div>
                
                <div className="addmovie-container-textarea">
                    <label htmlFor="">Movie Synopsis</label>
                    <textarea name="" id="" style={{resize:'none'} } placeholder="Enter synopsis here.." className="AddMovie-textarea"></textarea>
                </div>

                <button className="add-movie-btn">Add Movie</button>
            </div>

        </>

    )
}
export default AddMovie