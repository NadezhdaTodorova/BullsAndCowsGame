using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BullsAndCows.Data
{
    public class ScoreStatisticsData
    {
        private ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ScoreStatisticsData(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SaveUserScore(int score, int numTries)
        {
            HighScoreStatistics scoreStatistics = new HighScoreStatistics();

            scoreStatistics.Score = score;
            scoreStatistics.NumTries = numTries;
            scoreStatistics.Username = _httpContextAccessor.HttpContext.User.Identity.Name;
            _dbContext.HighScores.Add(scoreStatistics);
            _dbContext.SaveChanges();
        }

        public void SaveTurn(UserTurn userTurn)
        {
            Guid g = Guid.NewGuid();
            userTurn.Id = g.ToString();
            userTurn.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            _dbContext.UserTurns.Add(userTurn);
            _dbContext.SaveChanges();
        }

        public UserTurn GetCurrentUserTurn()
        {
            UserTurn userTurn = new UserTurn();
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            userTurn = _dbContext.UserTurns.ToList().Where(x => x.UserName == userName).FirstOrDefault();

            return userTurn;
        }

        public List<UserTurn> GetCurrentUserTurns(string gameId)
        {
            List<UserTurn> listUserTurn = new List<UserTurn>();
            string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            listUserTurn = _dbContext.UserTurns.Where(x => x.UserName == userName && x.GameId == gameId).OrderByDescending(x => x.LeftTries).ToList();

            return listUserTurn;
        }

        public List<HighScoreStatistics> GetHighScoreStatistics()
        {
            List<HighScoreStatistics> listHighScoreStatistics = _dbContext.HighScores.ToList();
            return listHighScoreStatistics;
        }
    }
}
