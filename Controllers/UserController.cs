using GameReviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GameReviewer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository<AppUser> _userRepository;
        private readonly IRepository<Review> _reviewRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Game> _gameRepository;

        public UserController(IHttpContextAccessor httpContextAccessor, IRepository<Review> reviewRepo, IUserRepository<AppUser> userRepo, IRepository<Game> gameRepo)
        {
            _userRepository = userRepo;
            _reviewRepository = reviewRepo;
            _gameRepository = gameRepo;
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

        public IActionResult AddReview(string user, int game, string reviewContent, string rating)
        {
            Review review = new Review()
            {
                User = _userRepository.GetByTitle(user),
                UserId = _userRepository.GetByTitle(user).Id,
                Game = _gameRepository.GetById(game),
                GameId = game,
                ReviewContent = reviewContent,
                Rating = float.Parse(rating, CultureInfo.InvariantCulture),
                CommentDate = DateTime.Now
            };
            if(_reviewRepository.Add(review))
                return View("/Views/Games/Details", game);
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
