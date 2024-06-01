/* 
Name: Sabrina Quadir 
Description: 
The Publisher.cs file defines the Publisher model in an ASP.NET Core web application. 
This model represents a game publishing company and includes properties such as 
-PublisherID
-Name
-Headquarters
-FoundingDate

The Publisher entity is used to store and manage data about game publishers within the database

 */

using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Headquarters { get; set; } = string.Empty;
        [Required]
        public DateTime FoundingDate { get; set; }
    }
}
