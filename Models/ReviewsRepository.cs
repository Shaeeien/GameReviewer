namespace GameReviewer.Models
{
    public class ReviewsRepository : IRepository<Review>
    {
        private readonly ReviewContext _reviewContext;
        
        public ReviewsRepository(ReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }
        public bool Add(Review entity)
        {
            if(entity != null)
            {
                _reviewContext.Reviews.Add(entity);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
               
        }

        public bool Exists(Review entity)
        {
            return _reviewContext.Reviews.Contains(entity);
        }

        public IEnumerable<Review> GetAll()
        {
            return _reviewContext.Reviews.ToList();
        }

        public Review? GetById(int id)
        {
            return _reviewContext.Reviews.Where(x => x.Id == id).FirstOrDefault(); 
        }

        public Review? GetByTitle(string title)
        {
            return _reviewContext.Reviews.Where(x => x.User.UserName == title).FirstOrDefault();
        }

        public int GetId(Review entity)
        {
            return entity.Id;
        }

        public bool Remove(Review entity)
        {
            var toRemove = _reviewContext.Reviews.Remove(entity);
            if(toRemove != null)
            {
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remove(int id)
        {
            Review toRemove = _reviewContext.Reviews.Where(x => x.Id == id).FirstOrDefault();
            if(toRemove != null)
            {
                _reviewContext.Reviews.Remove(toRemove);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
