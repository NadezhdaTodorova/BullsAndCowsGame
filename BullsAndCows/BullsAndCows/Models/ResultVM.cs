using System.Collections.Generic;

namespace BullsAndCows
{
    public class ResultVM : Digit
    {
        public int leftTries { get; set; }
        public bool playedGame { get; set; }
        public string[] resultMessage { get; set; }

        public List<UserTurn> listUserTurns { get; set; } = new List<UserTurn>();
    }
}
