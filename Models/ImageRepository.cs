namespace GameReviewer.Models
{
    public class ImageRepository : IRepository<Image>
    {
        private readonly ReviewContext _reviewContext;
        public ImageRepository(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
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

        public bool Exists(Image entity)
        {
            return _reviewContext.Images.Contains(entity);
        }

        public IEnumerable<Image> GetAll()
        {
            return _reviewContext.Images;
        }

        public Image? GetById(int id)
        {
            return _reviewContext.Images.Find(id);
        }

        public Image? GetByTitle(string title)
        {
            return _reviewContext.Images.Where(x => x.Name == title).FirstOrDefault();
        }

        public int GetId(Image entity)
        {
            return entity.Id;
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
    }
}
