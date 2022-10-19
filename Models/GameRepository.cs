namespace GameReviewer.Models
{
    public class GameRepository : IRepository<Game>
    {
        private readonly ReviewContext _reviewContext;

        public GameRepository()
        {
            _reviewContext = new ReviewContext();
        }
        public void Add(Game entity)
        {
            _reviewContext.Games.Add(entity);
        }

        public IEnumerable<Game> GetAll()
        {
            return _reviewContext.Games;
        }

        public Game? GetById(int id)
        {
            return _reviewContext.Games.Find(id);
        }

        public Game? GetByTitle(string title)
        {
            return _reviewContext.Games.Where(x => x.Name == title).FirstOrDefault();
        }

        public bool Exists(Game entity)
        {
            return _reviewContext.Games.Contains(entity);
        }

        public void Remove(Game entity)
        {
            _reviewContext.Games.Remove(entity);
            _reviewContext.SaveChanges();
        }

        public void Remove(int id)
        {
            Game? gameToRemove = _reviewContext.Games.Where(x => x.Id == id).FirstOrDefault();
            if(gameToRemove != null)
            {
                _reviewContext.Games.Remove(gameToRemove);
                _reviewContext.SaveChanges();
            }
                
        }

        public void Update(int id, Game updatedEntity)
        {
            Game? gameToUpdate = _reviewContext.Games.Where(x => x.Id == id).FirstOrDefault();
            if(gameToUpdate != null)
            {
                gameToUpdate.Reviews = updatedEntity.Reviews;
                gameToUpdate.Categories = updatedEntity.Categories;
                gameToUpdate.AvgPlayTimeInMins = updatedEntity.AvgPlayTimeInMins;
                gameToUpdate.Description = updatedEntity.Description;
                gameToUpdate.GameImages = updatedEntity.GameImages;
                gameToUpdate.Name = updatedEntity.Name;
                _reviewContext.SaveChanges();
            }
        }
    }
}
