using AutoMapper;
using MovieRating.Models;
using MovieRating.ViewModels;
using StarWarsApiCSharp;

namespace MovieRating.Helpers
{
    public static class MapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Film, MovieRatingViewModel>();
                cfg.CreateMap<MovieRatingViewModel, MovieRatingModel>();
            });
            var mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
