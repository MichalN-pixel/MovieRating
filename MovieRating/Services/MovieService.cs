using AutoMapper;
using MovieRating.Models;
using MovieRating.ViewModels;
using StarWarsApiCSharp;
using System.Collections.Generic;
using System.Linq;

namespace MovieRating.Services
{
    public interface IMovieService
    {
        void AddMovieRating(MovieRatingViewModel movieRatingViewModel);

        MovieRatingListViewModel GetMovieRatingListViewModel();
    }
    public class MovieService : IMovieService
    {
        private readonly IMovieRatingRepository _movieRepository;

        private readonly IMapper _mapper;
        public MovieService(IMovieRatingRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public void AddMovieRating(MovieRatingViewModel movieRatingViewModel)
        {
            var movieRatingModel = _mapper.Map<MovieRatingViewModel, MovieRatingModel>(movieRatingViewModel);
            _movieRepository.Add(movieRatingModel);
        }

        public MovieRatingListViewModel GetMovieRatingListViewModel()
        {
            var films = _movieRepository.GetAllFilms();
            var movieRatingViewModels = _mapper.Map<List<Film>, List<MovieRatingViewModel>>(films);
            var movieRatings = _movieRepository.GetMovieRatingModels();

            var movieRatingsFromDb = movieRatings
                .GroupBy(x => x.EpisodeId)
                .Select(x => new MovieRatingViewModel
                {
                    EpisodeId = x.Key,
                    Rating = x.Average(y => y.Rating)
                })
                .ToList();

            foreach (var movieRating in movieRatingsFromDb)
            {
                var movieRatingViewModel = movieRatingViewModels
                    .FirstOrDefault(x => x.EpisodeId == movieRating.EpisodeId);

                if(!(movieRatingViewModel is null))
                    movieRatingViewModel.Rating = movieRating.Rating;
            }

            return new MovieRatingListViewModel
            {
                MovieRatingViewModels = movieRatingViewModels
            };
        }
    }
}
