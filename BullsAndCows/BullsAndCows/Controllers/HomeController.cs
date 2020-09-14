using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BullsAndCows.Models;
using BullsAndCows.Interfaces;
using System.Security.Claims;
using BullsAndCows.Data;

namespace BullsAndCows.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGame _gameService;
        ScoreStatisticsData _scoreStatisticsData;
        public HomeController(ILogger<HomeController> logger, IGame gameService, ScoreStatisticsData scoreStatisticsData)
        {
            _logger = logger;
            _gameService = gameService;
            _scoreStatisticsData = scoreStatisticsData;
        }

        public IActionResult Index()
        {
            ResultVM result = new ResultVM();
            result.playedGame = false;
            result.leftTries = (int)Enums.Tries.initialValue;
            return View(result);
        }

        public IActionResult Play(Digit digits)
        {
            ResultVM result;
            ClaimsPrincipal currentUser = User;

            result = _gameService.PlayGame(digits, currentUser);
            if (result.leftTries == (int)Enums.Tries.endValue)
            {
                ModelState.Clear();
            }
            
            return View("./Index", result);
        }

        public IActionResult HightScoreStatistics()
        {
            HightScoresStatisticsVM scoreStatistics = new HightScoresStatisticsVM();
             scoreStatistics.listScoresStatistics = _scoreStatisticsData.GetHighScoreStatistics();
            return View(scoreStatistics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
