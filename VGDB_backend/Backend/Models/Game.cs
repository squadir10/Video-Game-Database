using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Game
{
    [Key]
    public int GameID { get; set; } // Primary keys generally should not be nullable.

    [Required]
    [StringLength(255)]
    public string? Title { get; set; } // Should not be nullable as it's required.

    [Column(TypeName = "date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? ReleaseDate { get; set; } // Nullable if the release date can be unknown.

    [StringLength(100)]
    public string? Genre { get; set; } // Nullable if the genre can be unknown.

    [StringLength(100)]
    public string? Platform { get; set; } // Nullable if the platform can be unknown.

    // Foreign keys should be nullable if the relationship is optional.
    [ForeignKey("Developer")]
    public int? DeveloperID { get; set; }
    

    [ForeignKey("Publisher")]
    public int? PublisherID { get; set; }

    // Navigation properties should be nullable if the relationship is optional.
    public Game()
    {
        GameReviews = new HashSet<GameReview>();
    }
    public Developer? Developer { get; set; }
    public Publisher? Publisher { get; set; }

    public virtual ICollection<GameReview> GameReviews { get; set; }
}
