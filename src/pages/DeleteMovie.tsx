
import { useEffect } from "react";
import {NavbarAdmin} from "../components/NavbarAdmin";



function DeleteMovie(){

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
            <div className="delete-container">
               
                <div className="addmovie-container-inner">
                    <label htmlFor="">Movie ID</label>
                    <input type="text" className="AddMovie-input"/>
                </div>
                <div className="addmovie-container-inner">
                   
                </div>
               
              
                <div className="addmovie-container-inner" style={{marginRight: '50%'}}>
                    <label htmlFor="">Theater ID</label>
                    <input type="text" className="AddMovie-input" />
                </div>

        

                <button className="delete-movie-btn">Delete Movie</button>
            </div>

        </>

    )
}
export default DeleteMovie