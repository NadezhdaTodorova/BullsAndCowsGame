using BullsAndCows.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BullsAndCows
{
    public class GameService : IGame
    {
        private Random _generateRandomNumber;
        private string generatedNumber;
        private int leftTries = (int)Enums.leftTries.initialValue;
        public GameService(Random generateRandomNumber)
        {
            _generateRandomNumber = generateRandomNumber;
            generatedNumber = GenerateNumber();
        }

        public ResultVM PlayGame(Digit digits)
        {
            ResultVM result = new ResultVM();
            
            string guessedNumber = digits.first.ToString() +
                digits.second.ToString() +
                digits.third.ToString() +
                digits.fourth.ToString();

            if (digits.newGame) {
                generatedNumber = GenerateNumber();
                leftTries = (int)Enums.leftTries.initialValue;
            }

            if (guessedNumber == generatedNumber)
            {
                result.resultMessage = FinishGame("User", int.Parse(generatedNumber));
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
            if (leftTries <= (int)Enums.leftTries.endValue)
            {
                result.resultMessage = FinishGame("Computer", int.Parse(generatedNumber));
            }

            return result;
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
            string num = _generateRandomNumber.Next(1000,10000).ToString();
            return num;
        }

        public string  FinishGame(string winner, int generatedNumber)
        {
            string message = "";
            if (winner == "User")
            {
                message = "Congratulations! You guessed the number. You score 50";
            }
            else if (winner == "Computer")
            {
                message = $"Sorry, the number was - {generatedNumber}";
            }
            return message;
        }
       
    }
}
