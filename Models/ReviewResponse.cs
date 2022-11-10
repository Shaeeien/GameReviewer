using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class ReviewResponse
    {
        [Key]
        public int Id { get; set; }
        public Review? Review { get; set; }
        public int? ReviewId { get; set; }
        public AppUser? User { get; set; }
        public int? UserId { get; set; }
        public string? ResponseContent { get; set; }
    }
}
