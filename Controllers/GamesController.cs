﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using GameReviewer.Models;
using System.Web;
using NuGet.Protocol.Plugins;

namespace GameReviewer.Controllers
{
    public class GamesController : Controller
    {
        private readonly ReviewContext _reviewContext;
        private GameRepository _repo;
        public GamesController()
        {
            _reviewContext = new ReviewContext();
            _repo = new GameRepository(_reviewContext);
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
            Game game = _repo.GetById(id);
            if(game != null)
                return View(game);
            return View("GameNotFound");
        }

        [HttpPost]
        public IActionResult AddGame(string name, string producer, int producerId, int avgLength, string description,[FromForm(Name = "images")] List<IFormFile> images)
        {
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
                if (_repo.Add(gameToAdd))
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

        public IActionResult RateGame(int gameId, string user, double rating, string review)
        {
            return View();
        }

        //public IActionResult GameDetails(Game game)
        //{
        //    if(game != null)
        //        return View(game);
        //    return View("GameNotFound");
        //}
    }
}
