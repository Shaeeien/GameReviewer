using GameReviewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = new UserRepository();
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
