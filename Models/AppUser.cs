using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Login jest wymagany")]
        public string UserName { get; set; } = "";
        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<ReviewResponse> ReviewResponses { get; set; } = new List<ReviewResponse>();

    }
}
