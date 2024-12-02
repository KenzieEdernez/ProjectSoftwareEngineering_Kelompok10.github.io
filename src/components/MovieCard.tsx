import React from "react";

interface MovieCardProps {
  imagePath: string;
  title: string;
}

export const MovieCard: React.FC<MovieCardProps> = ({ imagePath, title }) => {
  return (
    <div className="flex flex-col justify-center items-center">
      <img className="w-60 rounded-lg" src={imagePath} alt={title} />
      <a className="text-center mt-2" href="#">{title}</a>
    </div>
  );
};
