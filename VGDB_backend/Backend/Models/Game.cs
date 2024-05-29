using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Game
    {
        public int GameID { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; } = string.Empty;
        [Required]
        public string Platform { get; set; } = string.Empty;
        [Required]
        public Developer Developer { get; set; } = new Developer();
        [Required]
        public Publisher Publisher { get; set; } = new Publisher();
        public List<GameReview> GameReviews { get; set; } = new List<GameReview>();
    }
}
