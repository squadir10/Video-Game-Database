/* 
Name: Sabrina Quadir 
Description: 
The GameReview.cs file defines the GameReview model in an ASP.NET Core web application. 
This model represents a review of a video game and includes properties such as 

-GameReviewID
-Score
-ReviewText
-ReviewDate

The GameReview entity is used to store and manage data about individual reviews, 
logging the reviewer's score, written review, and the date the review was created.

 */

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
