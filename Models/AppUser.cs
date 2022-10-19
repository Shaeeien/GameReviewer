namespace GameReviewer.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
        public List<Review> Reviews { get; set; } = new List<Review>();

    }
}
