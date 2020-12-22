using AutoMapper;
using Moq;
using MovieRating.Helpers;
using MovieRating.Services;
using NUnit.Framework;
using StarWarsApiCSharp;
using System.Collections.Generic;

namespace MovieRating.UnitTests
{
    public class MovieServiceTests
    {
        private Mock<IMovieRatingRepository> _movieRepositoryMock;
        private IMapper _mapper;
        private double _firstRating = 1;
        private double _secondRating = 10;

        [SetUp]
        public void Setup()
        {
            _mapper = MapperHelper.GetMapper();

            _movieRepositoryMock = new Mock<IMovieRatingRepository>();
            _movieRepositoryMock
                .Setup(x => x.GetAllFilms(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Film> { new Film { EpisodeId = "1" } });

            _movieRepositoryMock
                .Setup(x => x.GetMovieRatingModels())
                .Returns(new List<Models.MovieRatingModel>
                {
                   new Models.MovieRatingModel{EpisodeId = "1", Rating = _firstRating },
                   new Models.MovieRatingModel{EpisodeId = "1", Rating = _secondRating },
                });
        }

        [Test]
        public void MovieService_Always_ReturnsAverageValueOfRating()
        {
            double expectedAverageValue = (_firstRating + _secondRating) / 2;
            
            var sut = new MovieService(_movieRepositoryMock.Object, _mapper);

            var result = sut.GetMovieRatingListViewModel();
            Assert.AreEqual(expectedAverageValue, result.MovieRatingViewModels[0].Rating);
        }
    }
}