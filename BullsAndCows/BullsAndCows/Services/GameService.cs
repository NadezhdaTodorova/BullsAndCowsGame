using BullsAndCows.Data;
using BullsAndCows.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BullsAndCows
{
    public class GameService : IGame
    {
        private Random _generateRandomNumber;
        private string generatedNumber;
        private int leftTries = (int)Enums.Tries.initialValue;

        public GameService(Random generateRandomNumber)
        {
            _generateRandomNumber = generateRandomNumber;
            generatedNumber = GenerateNumber();
        }

        public ResultVM PlayGame(Digit digits, ClaimsPrincipal currentUser)
        {
            ResultVM result = new ResultVM();
            
            string guessedNumber = digits.first.ToString() +
                digits.second.ToString() +
                digits.third.ToString() +
                digits.fourth.ToString();

            if (!DistinctDigits(guessedNumber) || digits.first < 1)
            {
                result.leftTries = (int)Enums.Tries.initialValue;
                result.resultMessage[0] = "There can be no repeating digits and the number cannot start with 0.";
                return result;
            }

            if (digits.newGame) {
                generatedNumber = GenerateNumber();
                leftTries = (int)Enums.Tries.initialValue;
            }

            if (guessedNumber == generatedNumber)
            {
                result.resultMessage = FinishGame("User", int.Parse(generatedNumber), leftTries);
                result.leftTries = (int)Enums.Tries.endValue;
            }
            else
            {
                int[] cowsAndBulls = CountBullsAndCows(guessedNumber, generatedNumber);
                result.cows = cowsAndBulls[0];
                result.bulls = cowsAndBulls[1];
                leftTries--;
                result.playedGame = true;
            }
            result.leftTries = leftTries;
            if (leftTries <= (int)Enums.Tries.endValue)
            {
                result.resultMessage = FinishGame("Computer", int.Parse(generatedNumber), leftTries);
            }

            return result;
        }

        private bool DistinctDigits(string guessedNumber)
        {
            return guessedNumber.Distinct().Count() == guessedNumber.Length;
        }

        private int[] CountBullsAndCows(string guessedNumber, string generatedNumber)
        {
            int bulls = 0;
            int cows = 0;

            bool[] isGuessVisted = new bool[generatedNumber.Length];
            bool[] isNumVisted = new bool[generatedNumber.Length];

            // count bulls and cows
            for (int i = 0; i < generatedNumber.Length; i++)
            {
                if (guessedNumber[i] == generatedNumber[i])
                {
                    bulls++;
                    isGuessVisted[i] = true;
                    isNumVisted[i] = true;
                }
            }

            for (int i = 0; i < guessedNumber.Length; i++)
            {
                for (int j = 0; j < generatedNumber.Length; j++)
                {
                    if (i != j &&
                        !isNumVisted[j] &&
                        !isGuessVisted[i] &&
                        guessedNumber[i] == generatedNumber[j])
                    {
                        cows++;
                        isGuessVisted[i] = true;
                        isNumVisted[j] = true;
                    }
                }
            }

            return new int[] { cows, bulls };
        }

        private string GenerateNumber()
        {
            string number = "";
            while (number.Length < 4)
            {
                string temp = _generateRandomNumber.Next(0, 9).ToString();
                if (number.Length == 0 && temp == "0")
                {
                    continue;
                }
                else if (number.Contains(temp))
                {
                    continue;
                }
                else
                {
                    number += temp;
                }
            }
            return number;
        }

        public string[]  FinishGame(string winner, int generatedNumber, int leftTries)
        {
            string[] message = new string [3];
            int tries = (int)Enums.Tries.initialValue - leftTries;
            if (winner == "User")
            {
                message[0] = "Congratulations! You guessed the number. You score 50";
                message[1] = "50";
                message[2] = $"{tries}";
            }
            else if (winner == "Computer")
            {
                message[0] = $"Sorry, the number was - {generatedNumber}";
            }
            return message;
        }

        

    }
}
