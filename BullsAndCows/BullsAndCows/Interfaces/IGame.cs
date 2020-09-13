using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BullsAndCows.Interfaces
{
    public interface IGame
    {
        public ResultVM PlayGame(Digit digits, ClaimsPrincipal currentUser);
        public string[] FinishGame(string winner, int generatedNumber, int tries);
    }
}
