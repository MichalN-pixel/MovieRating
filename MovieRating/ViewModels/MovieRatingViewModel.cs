using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRating.ViewModels
{
    public class MovieRatingListViewModel
    {
        public List<MovieRatingViewModel> MovieRatingViewModels = new List<MovieRatingViewModel>();
    }
    public class MovieRatingViewModel
    {
        public string EpisodeId { get; set; }

        public string Title { get; set; }

        public string OpeningCrawl { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }
    }
}
