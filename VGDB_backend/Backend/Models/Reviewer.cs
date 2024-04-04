using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Reviewers")]
public class Reviewer
{
    [Key]
    public int? ReviewerID { get; set; }

    [Required]
    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Affiliation { get; set; }

    [Range(0, int.MaxValue)]
    public int? ExperienceYears { get; set; }
}
