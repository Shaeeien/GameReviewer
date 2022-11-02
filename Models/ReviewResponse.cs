namespace GameReviewer.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public Review? Review { get; set; }
        public int? ReviewId { get; set; }
        public AppUser? User { get; set; }
        public int? UserId { get; set; }
        public string? ResponseContent { get; set; }
    }
}
