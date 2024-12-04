interface MallCardProps{
    images: string,
    mallName: string,
    location: string
  }
function MallCard({images, mallName, location} : MallCardProps){
    return(
   
            <div className="mall-card">
                        <a href=""><img src={images} alt="" /></a>
                        <h3>{mallName}</h3>
                        <p>{location}</p>
            </div>
      
    )
}
export default MallCard