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
