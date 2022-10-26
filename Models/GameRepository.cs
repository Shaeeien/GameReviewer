namespace GameReviewer.Models
{
    public class GameRepository : IRepository<Game>, IDisposable
    {
        private readonly ReviewContext _reviewContext;

        public GameRepository(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }
        public bool Add(Game entity)
        {
            if (GetByTitle(entity.Name) == null)
            {
                _reviewContext.Games.Add(entity);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Game> GetAll()
        {
            return _reviewContext.Games;
        }

        public Game? GetById(int id)
        {
            return _reviewContext.Games.Where(x => x.Id == id).First();
        }

        public Game? GetByTitle(string title)
        {
            return _reviewContext.Games.Where(x => x.Name == title).FirstOrDefault();
        }

        public bool Exists(Game entity)
        {
            return _reviewContext.Games.Contains(entity);
        }

        public bool Remove(Game entity)
        {
            var removedGame  = _reviewContext.Games.Remove(entity);
            if (removedGame != null)
            {
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
                
        }

        public bool Remove(int id)
        {
            Game? gameToRemove = _reviewContext.Games.Where(x => x.Id == id).FirstOrDefault();
            if(gameToRemove != null)
            {
                _reviewContext.Games.Remove(gameToRemove);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, Game updatedEntity)
        {
            Game? gameToUpdate = _reviewContext.Games.Where(x => x.Id == id).FirstOrDefault();
            if(gameToUpdate != null)
            {
                gameToUpdate.Reviews = updatedEntity.Reviews;
                gameToUpdate.Categories = updatedEntity.Categories;
                gameToUpdate.AvgPlayTimeInHours = updatedEntity.AvgPlayTimeInHours;
                gameToUpdate.Description = updatedEntity.Description;
                gameToUpdate.GameImages = updatedEntity.GameImages;
                gameToUpdate.Name = updatedEntity.Name;
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetId(Game entity)
        {
            return entity.Id;
        }

        public void Dispose()
        {
            _reviewContext?.Dispose();
        }
    }
}
