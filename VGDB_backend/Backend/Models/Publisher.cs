using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Publishers")]
public class Publisher
{
   [Key]
    public int? PublisherID { get; set; }

    [Required]
    [StringLength(255)]
    public string? Name { get; set; }  // Made nullable

    [StringLength(255)]
    public string? Headquarters { get; set; }  // Made nullable

    [Column(TypeName = "date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? FoundingDate { get; set; }
}
