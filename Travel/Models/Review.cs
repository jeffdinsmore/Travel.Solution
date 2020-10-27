using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }
  }
}