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
        private ApplicationDbContext _dbContext;
        private ScoreStatisticsData _scoreStatistics;

        public HomeController(ILogger<HomeController> logger, IGame gameService, ApplicationDbContext dbContext,
            ScoreStatisticsData scoreStatisticsData)
        {
            _logger = logger;
            _gameService = gameService;
            _dbContext = dbContext;
            _scoreStatistics = scoreStatisticsData;
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
            if(result.resultMessage != null && result.resultMessage[1] == "50")
            {
                _scoreStatistics.SaveUserScore(50, result.leftTries);
            }

            return View("./Index", result);
        }

        public IActionResult HightScoreStatistics()
        {
            List<HighScoreStatistics> listScoreStatistics = new List<HighScoreStatistics>();
            listScoreStatistics = _dbContext.HighScores.ToList();

            HightScoresStatisticsVM hightScoresStatisticsVM = new HightScoresStatisticsVM();
            hightScoresStatisticsVM.listScoresStatistics = listScoreStatistics;
            return View(hightScoresStatisticsVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
