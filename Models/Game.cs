using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int AvgPlayTimeInMins { get; set; }
        public string Description { get; set; } = "";
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Image> GameImages { get; set; } = new List<Image>();

    }
}