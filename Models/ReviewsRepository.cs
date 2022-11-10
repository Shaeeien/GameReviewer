using Microsoft.AspNetCore.Razor.Language;

namespace GameReviewer.Models
{
    public class ReviewsRepository : IRepository<Review>, IDisposable
    {
        private readonly ReviewContext _reviewContext;
        
        public ReviewsRepository(ReviewContext ctx)
        {
            _reviewContext = ctx;
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

        public bool AddResponse(ReviewResponse response)
        {
            if(response != null)
            {
                _reviewContext.ReviewResponses.Add(response);
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<ReviewResponse> GetResponses(Review review)
        {
            return _reviewContext.ReviewResponses.Where(x => x.ReviewId == review.Id);
        }

        public void Dispose()
        {
            _reviewContext?.Dispose();
        }

        public bool Exists(Review entity)
        {
            return _reviewContext.Reviews.Contains(entity);
        }

        public IEnumerable<Review> GetAll()
        {
            return _reviewContext.Reviews.ToList();
        }

        public IEnumerable<Review> GetByGame(Game game)
        {
            return _reviewContext.Reviews.Where(x => x.GameId == game.Id);
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

        public bool Update(int id, Review updatedEntity)
        {
            Review? reviewToUpdate = _reviewContext.Reviews.Where(x => x.Id == id).FirstOrDefault();
            if (reviewToUpdate != null)
            {
                reviewToUpdate.ReviewContent = updatedEntity.ReviewContent;
                reviewToUpdate.User = updatedEntity.User;
                reviewToUpdate.Responses = updatedEntity.Responses;
                reviewToUpdate.UserId = updatedEntity.UserId;
                reviewToUpdate.CommentDate = updatedEntity.CommentDate;
                reviewToUpdate.Game = updatedEntity.Game;
                reviewToUpdate.GameId = updatedEntity.GameId;                
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
