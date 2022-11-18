using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using GameReviewer.Models;
using System.Web;
using NuGet.Protocol.Plugins;

namespace GameReviewer.Controllers
{
    public class GamesController : Controller
    {
        private readonly IRepository<Game> _gameRepo;
        private readonly IRepository<Producer> _producerRepo;
        private readonly IRepository<Image> _imageRepo;
        public GamesController(IRepository<Game> gameRepo, IRepository<Producer> producerRepo, IRepository<Image> imageRepo)
        {
            _gameRepo = gameRepo;
            _producerRepo = producerRepo;
            _imageRepo = imageRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GamesList()
        {
            return View();
        }

        public IActionResult AddGameForm()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Game game = _gameRepo.GetById(id);
            if(game != null)
                return View(game);
            return View("GameNotFound");
        }

        [HttpPost]
        public IActionResult AddGame(string name, string producer, int producerId, int avgLength, string description,[FromForm(Name = "images")] List<IFormFile> images)
        {
            List<Image> img = new List<Image>();
            Game gameToAdd = new Game
            {
                Name = name,
                Producer = _producerRepo.GetByTitle(producer),
                ProducerId = producerId,
                Description = description,
                AvgPlayTimeInHours = avgLength,
                GameImages = new List<Image>()
            }; 

            if(images != null)
            {
                foreach (IFormFile file in images)
                {
                    if(file != null)
                    {
                        if(file.Length > 0)
                        {
                            byte[] fileBytes = null;
                            using(var fs = file.OpenReadStream())
                            using(var ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                fileBytes = ms.ToArray();                               
                                Image toAdd = new Image()
                                {
                                    Name = file.Name,
                                    Data = fileBytes,
                                    Game = gameToAdd,
                                    GameId = gameToAdd.Id
                                };
                                gameToAdd.GameImages.Add(toAdd);
                                _imageRepo.Add(toAdd);
                            }
                        }
                    }
                }
                if (_gameRepo.Add(gameToAdd))
                    return View("AddingSuccessful", gameToAdd);
            }            
            return View("AddingFailed", gameToAdd);
        }

        public IActionResult RemoveGame()
        {
            return View();
        }

        public IActionResult UpdateGame()
        {
            return View();
        }
    }
}
