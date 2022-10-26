using GameReviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewer.Controllers
{
    public class UserController : Controller, IPasswordHasher<AppUser>
    {
        private readonly UserRepository _userRepository;
        private PasswordHasher<AppUser> _passwordHasher;
        public UserController()
        {
            _userRepository = new UserRepository();
            _passwordHasher = new PasswordHasher<AppUser>();
        }

        public string HashPassword(AppUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
        {
            return VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string login, string password)
        {
            if(_userRepository.Login(login, password))
            {
                HttpContext.Session.SetString("Login", login);
                return View();
            }
            return View("LoinFailed");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();           
            return View();
        }

        public IActionResult Register(string login, string password)
        {
            AppUser newUser = new AppUser()
            {
                UserName = login,
                Password = password,
                IsAdmin = false,
                Reviews = new List<Review>()
            };
            if (!_userRepository.Exists(newUser))
            {
                string hashedPassword = HashPassword(newUser, password);
                newUser.Password = hashedPassword;
                _userRepository.Add(newUser);
                return View("RegistrationSuccess");
            }             
            return View("RegistrationFailed");
        }
    }
}
