using System.ComponentModel.DataAnnotations;

namespace GameReviewer.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
