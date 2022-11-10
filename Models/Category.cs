using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
    }
}
