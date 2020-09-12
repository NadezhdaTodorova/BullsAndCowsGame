using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BullsAndCows.Interfaces
{
    public interface IGame
    {
        public ResultVM PlayGame(Digit digits);
        public string FinishGame(string winner, int generatedNumber);
    }
}
