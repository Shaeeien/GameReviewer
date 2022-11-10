using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public List<Game> Games { get; set; }
    }
}
