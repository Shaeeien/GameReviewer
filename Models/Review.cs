namespace GameReviewer.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string ReviewContent { get; set; } = "";
        public float Rating { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
