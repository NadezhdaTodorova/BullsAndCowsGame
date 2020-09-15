using System.Security.Claims;

namespace BullsAndCows.Interfaces
{
    public interface IGame
    {
        public ResultVM PlayGame(Digit digits, ClaimsPrincipal currentUser);
        public string[] FinishGame(string winner, string generatedNumber, int tries);
    }
}
