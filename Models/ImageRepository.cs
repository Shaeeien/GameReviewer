namespace GameReviewer.Models
{
    public class ImageRepository : IRepository<Image>, IDisposable
    {
        private readonly ReviewContext _reviewContext;
        public ImageRepository()
        {
            _reviewContext = new ReviewContext();
        }

        public bool Add(Image entity)
        {
            if(!Exists(entity) && entity != null)
            {
                _reviewContext.Images.Add(entity);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            _reviewContext.Dispose();
        }

        public bool Exists(Image entity)
        {
            return _reviewContext.Images.Contains(entity);
        }

        public IEnumerable<Image> GetAll()
        {
            return _reviewContext.Images.ToList();
        }

        public Image? GetById(int id)
        {
            return _reviewContext.Images.Where(x => x.Id == id).FirstOrDefault();
        }

        public Image? GetByTitle(string title)
        {
            return _reviewContext.Images.Where(x => x.Name == title).FirstOrDefault();
        }

        public int GetId(Image entity)
        {
            return entity.Id;
        }

        public List<Image> GetByGame(Game game)
        {
            return GetAll().Where(x => x.GameId == game.Id).ToList();
        }

        public void RemoveByGame(Game game)
        {
            foreach (Image img in game.GameImages)
                _reviewContext.Images.Remove(img);
        }

        public bool Remove(Image entity)
        {
            var removedImage = _reviewContext.Images.Remove(entity);
            if (removedImage != null)
            {
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remove(int id)
        {
            Image? imageToRemove = _reviewContext.Images.Where(x => x.Id == id).FirstOrDefault();
            if (imageToRemove != null)
            {
                _reviewContext.Images.Remove(imageToRemove);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, Image updatedEntity)
        {
            Image? imageToUpdate = _reviewContext.Images.Where(x => x.Id == id).FirstOrDefault();
            if (imageToUpdate != null)
            {
                imageToUpdate.Name = updatedEntity.Name;
                imageToUpdate.Data = updatedEntity.Data;
                imageToUpdate.Game = updatedEntity.Game;
                imageToUpdate.GameId = updatedEntity.GameId;
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ExistsById(int id)
        {
            Image img = _reviewContext.Images.FirstOrDefault(x => x.Id == id);
            if (img != null)
                return true;
            return false;
        }
    }
}
