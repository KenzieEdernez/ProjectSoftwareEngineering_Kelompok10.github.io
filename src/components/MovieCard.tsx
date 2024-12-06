import React from "react";
import { Link } from "react-router-dom";

interface MovieCardProps {
  imagePath: string;
  title: string;
}

export const MovieCard: React.FC<MovieCardProps> = ({ imagePath, title }) => {
  return (
    <div className="flex flex-col justify-center items-center">
      <Link
        to={`/moviedesc/${title}`}
        state={{ imagePath }} 
        className="text-center mt-2 cursor-pointer font-semibold"
      >
        <img className="rounded-lg cursor-pointer" src={imagePath} alt={title} />
      </Link>
      <Link
        to={`/moviedesc/${title}`}
        state={{ imagePath }}
        className="text-center mt-2 cursor-pointer font-semibold"
      >
        {title}
      </Link>
    </div>
  );
};
