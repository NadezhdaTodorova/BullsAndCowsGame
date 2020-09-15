using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            ResultVM result = new ResultVM();
            ClaimsPrincipal currentUser = User;

            if(digits.first > 9 || digits.first < 0 
                || digits.second > 9 || digits.second < 0 ||
                digits.third > 9 || digits.third < 0 || 
                digits.fourth > 9 || digits.fourth < 0)
            {
                result.resultMessage = new string[3] { 
                    "Please provide digits between 0 and 9",
                    "", "" };
            }
            else
            {
                result = _gameService.PlayGame(digits, currentUser);
                if (result.leftTries == (int)Enums.Tries.endValue)
                {
                    ModelState.Clear();
                }
            }

            return View("./Index", result);
        }

        public IActionResult HightScoreStatistics()
        {
            HightScoresStatisticsVM scoreStatistics = new HightScoresStatisticsVM();
             scoreStatistics.listScoresStatistics = _scoreStatisticsData.GetHighScoreStatistics();
            return View(scoreStatistics);
        }
    }
}
