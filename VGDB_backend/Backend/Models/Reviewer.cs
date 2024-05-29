using System.ComponentModel.DataAnnotations;

namespace VideoGameDatabase.Models
{
    public class Reviewer
    {
        public int ReviewerID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Affiliation { get; set; }
        public int? ExperienceYears { get; set; }
    }
}
