namespace GameReviewer.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public List<Game> Games { get; set; }
    }
}
