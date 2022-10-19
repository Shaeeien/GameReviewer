namespace GameReviewer.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
    }
}
