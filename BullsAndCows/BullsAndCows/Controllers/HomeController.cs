using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BullsAndCows.Models;
using BullsAndCows.Interfaces;

namespace BullsAndCows.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGame _gameService;

        public HomeController(ILogger<HomeController> logger, IGame gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            ResultVM result = new ResultVM();
            result.playedGame = false;
            result.leftTries = (int)Enums.leftTries.initialValue;
            return View(result);
        }

        public IActionResult Play(Digit digits)
        {
            ResultVM result = new ResultVM();
            if (digits.first > 0)
            {
                result = _gameService.PlayGame(digits);
                if (result.leftTries == (int)Enums.leftTries.endValue)
                {
                    ModelState.Clear();
                }
            }
            else
            {
                result.leftTries = (int)Enums.leftTries.initialValue;
                result.resultMessage = "Please provide first digit greater than 0";
            }
            return View("./Index", result);
        }

        public IActionResult HightScoreStatistics()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
