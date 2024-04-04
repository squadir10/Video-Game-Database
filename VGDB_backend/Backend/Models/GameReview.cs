using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("GameReviews")]
public class GameReview
{
    [Key]
    public int? GameReviewID { get; set; }

    [Required]
    public int? GameID { get; set; }

    [ForeignKey("GameID")]
    public Game? Game { get; set; }

    [Required]
    public int? ReviewerID { get; set; }

    [ForeignKey("ReviewerID")]
    public Reviewer? Reviewer { get; set; }

    [Range(1, 10)]
    public int? Score { get; set; }

    [Column(TypeName = "text")]
    public string? ReviewText { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ReviewDate { get; set; }
}