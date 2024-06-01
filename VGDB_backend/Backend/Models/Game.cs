/* 
Name: Sabrina Quadir 
Description: 
The Game.cs file defines the Game model in an ASP.NET Core web application. 
It is a vital file for this database

This model represents a video game and includes properties such as:
-GameID
-Title
-ReleaseDate
-Genre
-Platform 

Additionally, the Game model has navigation properties to link it to its developer, publisher, and associated reviews. 
The Game entity is used to store and manage data about video games within the database, 
capturing essential information and relationships with other entities.

 */

using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Game
    {
        public int GameID { get; set; }
        //had to put [required] as it is mandatory to fill in all fields properly 
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
