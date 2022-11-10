using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string ReviewContent { get; set; } = "";
        public float Rating { get; set; }
        public List<ReviewResponse> Responses { get; set; } = new List<ReviewResponse>();
        public DateTime CommentDate { get; set; }
    }
}
