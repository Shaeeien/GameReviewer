using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using GameReviewer.Models;
using System.Web;

namespace GameReviewer.Controllers
{
    public class GamesController : Controller
    {
        private readonly ReviewContext _reviewContext;
        public GamesController()
        {
            _reviewContext = new ReviewContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GamesList()
        {
            GameRepository gameRepository = new GameRepository(_reviewContext); 
            return View(gameRepository.GetAll());
        }

        public IActionResult AddGameForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGame(string name, string producer, int producerId, int avgLength, string description,[FromForm(Name = "images")] List<IFormFile> images)
        {
            GameRepository gameRepository = new GameRepository(_reviewContext);
            ProducerRepository producerRepository = new ProducerRepository(_reviewContext);
            ImageRepository imageRepository = new ImageRepository(_reviewContext);
            List<Image> img = new List<Image>();
            Game gameToAdd = new Game
            {
                Name = name,
                Producer = producerRepository.GetByTitle(producer),
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
                                _reviewContext.Images.Add(toAdd);
                            }
                        }
                    }
                }
                if (gameRepository.Add(gameToAdd))
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

        public IActionResult GameDetails(Game game)
        {
            if(game != null)
                return View(game);
            return View("GameNotFound");
        }
    }
}
