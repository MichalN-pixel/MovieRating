using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieRating.Models;
using MovieRating.Services;
using MovieRating.ViewModels;
using System.Diagnostics;

namespace MovieRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var movieRatingViewModels = _movieService.GetMovieRatingListViewModel();
            return View(movieRatingViewModels);
        }

        [HttpPost]
        public IActionResult Index(MovieRatingViewModel movieRatingViewModel)
        {
            if (ModelState.IsValid)
            {
                _movieService.AddMovieRating(movieRatingViewModel);
            }
            var movieRatingViewModels = _movieService.GetMovieRatingListViewModel();
            return View(movieRatingViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
