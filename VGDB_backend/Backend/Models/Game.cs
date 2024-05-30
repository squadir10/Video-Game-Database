using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Game
    {
        public int GameID { get; set; }
        
        [Required]
        public required string Title { get; set; }
        
        [Required]
        public required DateTime ReleaseDate { get; set; }
        
        [Required]
        public required string Genre { get; set; }
        
        [Required]
        public required string Platform { get; set; }
        
        [Required]
        public required Developer Developer { get; set; } = new Developer();
        
        [Required]
        public required Publisher Publisher { get; set; } = new Publisher();
        
        public List<GameReview> GameReviews { get; set; } = new List<GameReview>();
    }
}
