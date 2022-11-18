using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Microsoft.Extensions.Identity.Core;

namespace GameReviewer.Models
{
    public class UserRepository : IUserRepository<AppUser>, IPasswordHasher<AppUser>, IDisposable
    {
        private readonly ReviewContext _reviewContext;
        private PasswordHasher<AppUser> _passwordHasher;

        public UserRepository()
        {
            _reviewContext = new ReviewContext();
            _passwordHasher = new PasswordHasher<AppUser>();
        }

        public string HashPassword(AppUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

        public bool Add(AppUser entity)
        {
            if (GetByTitle(entity.UserName) == null)
            {
                entity.Reviews = new List<Review>();
                entity.IsAdmin = false;
                string hashedPassword = HashPassword(entity, entity.Password);
                entity.Password = hashedPassword;
                _reviewContext.Add(entity);
                _reviewContext.SaveChanges();
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
            return _reviewContext.AppUsers.Where(x => x.UserName == title).FirstOrDefault();
        }

        public bool Exists(AppUser entity)
        {
            if (_reviewContext.AppUsers.Where(x => x.UserName == entity.UserName).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        public bool Remove(AppUser entity)
        {
            var removedUser = _reviewContext.AppUsers.Remove(entity);
            if (removedUser != null)
            {
                _reviewContext.SaveChanges();
                return true;
            }               
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

        public int GetId(AppUser entity)
        {
            return entity.Id;
        }

        public bool Login(string login, string password)
        {
            AppUser user = _reviewContext.AppUsers.SingleOrDefault(x => x.UserName == login);
            if(user != null)
            {
                PasswordVerificationResult res = VerifyHashedPassword(user, user.Password, password);
                if (res == PasswordVerificationResult.Success)
                    return true;
            }           
            return false;
        }

        public void Dispose()
        {
            _reviewContext.Dispose();
        }

        public bool ExistsById(int id)
        {
            AppUser user = _reviewContext.AppUsers.First(x => x.Id == id);
            if (user != null)
                return true;
            return false;
        }
    }
}
