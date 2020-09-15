using System.ComponentModel.DataAnnotations;

namespace BullsAndCows
{
    public class HighScoreStatistics
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public int NumTries { get; set; }
    }
}
