using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RatingDto
    {
        [Required]
        [Range(1, 10, ErrorMessage = "Value for {0} mest be between {1} and {2}")]
        public int Score { get; set; }
    }
}