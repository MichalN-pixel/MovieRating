using MovieRating.DAL;
using MovieRating.Models;
using StarWarsApiCSharp;
using System.Collections.Generic;
using System.Linq;

namespace MovieRating.Services
{
    public interface IMovieRatingRepository
    {
        List<Film> GetAllFilms(int page = 1, int size = 100);

        void Add(MovieRatingModel movieRatingModel);

        List<MovieRatingModel> GetMovieRatingModels();
    }

    public class MovieRatingRepository : IMovieRatingRepository
    {
        private readonly Repository<Film> _filmRepository;
        private readonly MoviesContext _moviesContext;

        public MovieRatingRepository(
            Repository<Film> filmRepository,
            MoviesContext moviesContext)
        {
            _filmRepository = filmRepository;
            _moviesContext = moviesContext;
        }

        public void Add(MovieRatingModel movieRatingModel)
        {
            _moviesContext.MovieRatings.Add(movieRatingModel);
            _moviesContext.SaveChanges();
        }

        public List<MovieRatingModel> GetMovieRatingModels()
        {
            return _moviesContext.MovieRatings.ToList();
        }

        public List<Film> GetAllFilms(int page = 1, int size = 100)
        { 
            var films = _filmRepository.GetEntities(page, size).ToList();
            
            return films;
        }


    }
}
