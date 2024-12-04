interface MallCardProps{
    mallName: string,
    location: string
  }
function MallCard({ mallName, location} : MallCardProps){
    return(
   
            <div className="mall-card">
                        <a href=""><h3>{mallName}</h3></a>
                        <p>{location}</p>
            </div>
      
    )
}
export default MallCard