using GameReviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly ReviewsRepository _reviewRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = new UserRepository();
            _reviewRepository = new ReviewsRepository();
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string login, string password)
        {
            if(_userRepository.Login(login, password))
            {
                if(_httpContextAccessor != null)
                {
                    if(_httpContextAccessor.HttpContext != null)
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("Login", login);
                        return View("LoginSuccess");
                    }                    
                }                
            }
            return View("LoginFailed");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();           
            return View();
        }

        [HttpPost]
        public IActionResult Register(AppUser user, string repeatedPassword, bool confirm)
        {
            if (confirm && user.Password == repeatedPassword)
            {
                if (!_userRepository.Exists(user))
                {
                    if(_userRepository.Add(user))
                        return View("RegistrationSuccess");
                }
            }                      
            return View("RegistrationFailed");
        }

        public IActionResult AddReview(AppUser user, Game game, string reviewContent, float rating)
        {
            Review review = new Review()
            {
                User = user,
                UserId = user.Id,
                Game = game,
                GameId = game.Id,
                ReviewContent = reviewContent,
                Rating = rating
            };
            if(_reviewRepository.Add(review))
                return View("ReviewAddSuccess");
            return View("ReviewAddFailure");
        }

        public IActionResult AddResponse(Review review, string response, AppUser respondingUser)
        {
            return View();
        }

        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        public IActionResult RegistrationFailed()
        {
            return View();
        }

        public IActionResult RegistrationPage()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
