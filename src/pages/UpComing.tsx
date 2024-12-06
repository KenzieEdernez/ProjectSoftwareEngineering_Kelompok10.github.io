import { MovieCard } from "../components/MovieCard";
import movie1 from "../assets/posters/bila_ibu_tiada.jpg";
import movie2 from "../assets/posters/gladiator2.jpg";
import movie3 from "../assets/posters/heretic.jpg";
import movie4 from "../assets/posters/moana2.jpg";
import Navbar from "../components/Navbar";

const movies = [
  { imagePath: movie1, title: "Movie 1" },
  { imagePath: movie2, title: "Movie 2" },
  { imagePath: movie3, title: "Movie 3" },
  { imagePath: movie4, title: "Movie 4" },
];

export const UpComing = () => {
  return (
    <>
      <Navbar />
      <div className="mx-40 mt-10">
        <h1 className="text-3xl text-start font-bold mb-10">UP COMING IN CINEMAS</h1>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
          {movies.map((movie, index) => (
            <MovieCard
              key={index}
              imagePath={movie.imagePath}
              title={movie.title}
            />
          ))}
        </div>
      </div>
    </>
  );
};
