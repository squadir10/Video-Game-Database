using System;
using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class GameReview
    {
        public int GameReviewID { get; set; }
        public int GameID { get; set; }
        public int ReviewerID { get; set; }
        public int? Score { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public DateTime? ReviewDate { get; set; }
        [Required]
        public Reviewer Reviewer { get; set; } = new Reviewer();
        [Required]
        public Game Game { get; set; } = new Game();
    }
}
