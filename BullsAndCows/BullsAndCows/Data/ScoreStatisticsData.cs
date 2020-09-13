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
    }
}
