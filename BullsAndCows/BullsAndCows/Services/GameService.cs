using BullsAndCows.Data;
using BullsAndCows.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BullsAndCows
{
    public class GameService : IGame
    {
        private Random _generateRandomNumber;
        private static string generatedNumber;
        private ScoreStatisticsData _scoreStatistics;
        private static string gameId;
        private static int leftTries;

        public GameService(Random generateRandomNumber,
            ScoreStatisticsData scoreStatisticsData)
        {
            _generateRandomNumber = generateRandomNumber;
            _scoreStatistics = scoreStatisticsData;
        }

        public ResultVM PlayGame(Digit digits, ClaimsPrincipal currentUser)
        {
            ResultVM result = new ResultVM();
            string guessedNumber = digits.first.ToString() +
                digits.second.ToString() +
                digits.third.ToString() +
                digits.fourth.ToString();

            if (!DistinctDigits(guessedNumber))
            {
                leftTries = (int)Enums.Tries.initialValue;
                result.resultMessage = new string[3];
                result.resultMessage[0] = "There can be no repeating digits!";
                return result;
            }

            if (digits.newGame) {
                Guid g = Guid.NewGuid();
                generatedNumber = GenerateNumber();
                leftTries = (int)Enums.Tries.initialValue;
                gameId = g.ToString();
            }

            if (guessedNumber == generatedNumber)
            {
                result.resultMessage = FinishGame("User", int.Parse(generatedNumber), leftTries);
                result.leftTries = (int)Enums.Tries.endValue;
                return result;
            }
            else
            {
                HandleTurn(guessedNumber);
                result.playedGame = true;
                result.leftTries = leftTries;
            }

            result.listUserTurns = _scoreStatistics.GetCurrentUserTurns(gameId);

            if (result.leftTries <= (int)Enums.Tries.endValue)
            {
                result.resultMessage = FinishGame("Computer", int.Parse(generatedNumber), leftTries);
            }

            return result;
        }

        private void HandleTurn(string guessedNumber)
        {
            UserTurn userTurn = new UserTurn();
            int[] cowsAndBulls = CountBullsAndCows(guessedNumber, generatedNumber);

            userTurn.Cows = cowsAndBulls[0];
            userTurn.Bulls = cowsAndBulls[1];
            userTurn.GuessedNumber = guessedNumber;
            userTurn.GeneratedNumber = generatedNumber;
            leftTries = leftTries - 1;
            userTurn.LeftTries = leftTries;
            userTurn.GameId = gameId;

            _scoreStatistics.SaveTurn(userTurn);
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
                if (number.Contains(temp))
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
                message[1] = Enums.WinnerScore.userWinScore.ToString();
                message[2] = $"{tries}";
                _scoreStatistics.SaveUserScore(50, tries);
            }
            else if (winner == "Computer")
            {
                message[0] = $"Sorry, the number was - {generatedNumber}";
            }
            return message;
        }

        

    }
}
