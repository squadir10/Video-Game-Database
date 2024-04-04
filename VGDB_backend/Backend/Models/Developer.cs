using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Developer
{
    [Key]
    public int? DeveloperID { get; set; }

    [Required]
    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }  // Made nullable

    [Column(TypeName = "date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? FoundingDate { get; set; }
}
