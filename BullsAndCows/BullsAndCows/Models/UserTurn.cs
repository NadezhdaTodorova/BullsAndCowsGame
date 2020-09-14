using System.ComponentModel.DataAnnotations;

namespace BullsAndCows
{
    public class UserTurn
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Bulls { get; set; }
        public int Cows { get; set; }
        public int LeftTries { get; set; }
        public string GuessedNumber { get; set; }
        public string GeneratedNumber { get; set; }
        public string GameId { get; set; }
    }
}
