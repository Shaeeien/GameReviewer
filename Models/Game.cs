using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int AvgPlayTimeInHours { get; set; }
        public string Description { get; set; } = "";
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Image> GameImages { get; set; } = new List<Image>();

    }
}