/* 
Name: Sabrina Quadir 
Description: 
The Reviewer.cs file defines the Reviewer model in an ASP.NET Core web application. 
This model represents a person or entity that reviews video games and includes properties such as 

-ReviewerID
-Name
-Affiliation
-ExperienceYears

The Reviewer entity is used to store and manage data about reviewers within the database, 
 */

using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Reviewer
    {
        public int ReviewerID { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Affiliation { get; set; } = string.Empty;
        
        public int ExperienceYears { get; set; }
    }
}
