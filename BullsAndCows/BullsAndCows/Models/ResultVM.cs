namespace BullsAndCows
{
    public class ResultVM : Digit
    {
        public int cows { get; set; }
        public int bulls { get; set; }
        public int leftTries { get; set; }
        public bool playedGame { get; set; }
        public string[] resultMessage { get; set; }
    }
}
