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
