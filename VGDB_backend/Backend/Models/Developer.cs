/* 
Name: Sabrina Quadir 
Description: 
The Developer.cs file defines the Developer model in an ASP.NET Core web application. 
This model represents a game development company and includes properties such as:
-DeveloperID
-Name
-Location
-FoundingDate

The Developer entity is used to store and manage data about game developers within the database, 
including their unique identifier, name, HQ location, and the date the company was founded.

 */

using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Developer
    {
        public int DeveloperID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public DateTime FoundingDate { get; set; }
    }
}
