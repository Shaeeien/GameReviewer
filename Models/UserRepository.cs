namespace GameReviewer.Models
{
    public class UserRepository : IRepository<AppUser>
    {
        private readonly ReviewContext _reviewContext;

        public UserRepository()
        {
            _reviewContext = new ReviewContext();
        }
        public bool Add(AppUser entity)
        {
            if (GetByTitle(entity.UserName) == null)
            {
                _reviewContext.Add(entity);
                return true;
            }
            return false;
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _reviewContext.AppUsers.ToList();
        }

        public AppUser? GetById(int id)
        {
            return _reviewContext.AppUsers.Find(id);
        }

        public AppUser GetByTitle(string title)
        {
            return _reviewContext.AppUsers.Where(x => x.UserName == title).First();
        }

        public bool Exists(AppUser entity)
        {
            if (_reviewContext.AppUsers.Contains(entity))
            {
                return true;
            }
            return false;
        }

        public bool Remove(AppUser entity)
        {
            var removedUser = _reviewContext.AppUsers.Remove(entity);
            if (removedUser != null)
                return true;
            return false;
        }

        public bool Remove(int id)
        {
            AppUser? userToRemove = _reviewContext.AppUsers.Find(id);
            if(userToRemove != null)
            {
                _reviewContext.AppUsers.Remove(userToRemove);
                return true;
            }
            return false;
        }

        public bool Update(int id, AppUser updatedUser)
        {
            AppUser? userToUpdate = _reviewContext.AppUsers.Where(x => x.Id == id).FirstOrDefault();
            if (userToUpdate != null)
            {
                userToUpdate.Reviews = updatedUser.Reviews;
                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.IsAdmin = updatedUser.IsAdmin;
                userToUpdate.Password = updatedUser.Password;
                _reviewContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
