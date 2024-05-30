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
        
        [Required]
        public required string ReviewText { get; set; }
        
        public DateTime? ReviewDate { get; set; }
        
        [Required]
        public required Reviewer Reviewer { get; set; } = new Reviewer();
    }
}
