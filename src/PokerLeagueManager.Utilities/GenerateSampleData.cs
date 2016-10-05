using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class GenerateSampleData
    {
        public static void Generate(string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Expected 3 arguments", "args");
            }

            var serviceUrl = args[1];
            var numberOfGames = int.Parse(args[2]);

            Console.WriteLine($"serviceUrl: {serviceUrl}");
            Console.WriteLine($"numberOfGames: {numberOfGames}");

            using (var svc = new CommandServiceProxy())
            {
                svc.SetUrl(serviceUrl);

                foreach (var cmd in GenerateSampleDataCommands(numberOfGames))
                {
                    Console.WriteLine($"Executing Command: {cmd.GetType().Name}");
                    svc.ExecuteCommand(cmd);
                }
            }
        }

        private static IEnumerable<EnterGameResultsCommand> GenerateSampleDataCommands(int numberOfGames)
        {
            var results = new List<EnterGameResultsCommand>();
            var r = new Random();

            for (int g = 0; g < numberOfGames; g++)
            {
                var newGame = new EnterGameResultsCommand();
                newGame.GameDate = GetUniqueDate(results);

                var numPlayers = r.Next(5, 15);
                var players = new List<EnterGameResultsCommand.GamePlayer>();
                var totalPot = 0;

                for (int p = 0; p < numPlayers; p++)
                {
                    var newPlayer = new EnterGameResultsCommand.GamePlayer();
                    newPlayer.PlayerName = GetRandomPlayerName(players);
                    newPlayer.Placing = p + 1;
                    newPlayer.PayIn = GetRandomPayIn();

                    totalPot += newPlayer.PayIn;

                    players.Add(newPlayer);
                }

                var thirdWinnings = (int)Math.Round((totalPot * 0.1) / 10) * 10;
                var secondWinnings = (int)Math.Round((totalPot * 0.3) / 10) * 10;
                var firstWinnings = totalPot - (thirdWinnings + secondWinnings);

                players[0].Winnings = firstWinnings;
                players[1].Winnings = secondWinnings;
                players[2].Winnings = thirdWinnings;

                newGame.Players = players;
                results.Add(newGame);
            }

            return results;
        }

        private static int GetRandomPayIn()
        {
            var possibleValues = new List<int>() { 20, 30, 40, 50, 60, 70 };

            var result = GenerateRandomInteger(possibleValues.Count);

            return possibleValues[result];
        }

        private static string GetRandomPlayerName(List<EnterGameResultsCommand.GamePlayer> players)
        {
            var playerNames = new List<string>()
            {
                "Dylan Smith",
                "Ryan Fritsch",
                "Sauce",
                "Shane Wilkins",
                "G.W. Stein",
                "Colin Hickson",
                "Grant Hirose",
                "Jeff",
                "Alex K",
                "Rob Schneider",
                "Sean Kehoe",
                "Meghan Mawhinney",
                "Ray Tara",
                "Sam Pearce",
                "Jason The",
                "Chris Wentz",
                "Kiana Lindsay",
                "Sherika Vollmer",
                "Alfred Rolando",
                "Chauncey Cavallaro",
                "Karl Brush",
                "Carlos Brumett",
                "Hwa Gensler",
                "Lynnette Levan",
                "Jovita Tongue",
                "Alyse Mauk",
                "Sanjuanita Zieman",
                "Glory Vanwagenen",
                "Betsy Vasques",
                "Elouise Allison",
                "Sheridan Oxner",
                "Sunni Cooke",
                "Ozell Funston",
                "Dorotha Winland",
                "Estelle Weibel",
                "Corazon Benware",
                "Mabelle Bopp",
                "Hope Byfield",
                "Romeo Winters",
                "Sasha Mongeau",
                "Ricki Westendorf",
                "Lane Clink",
                "Max Chesnutt",
                "Demetrius Reighard",
                "Suzanna Basel",
                "Claud Caverly"
            };

            var result = GenerateRandomInteger(playerNames.Count);

            while (players.Any(p => p.PlayerName == playerNames[result]))
            {
                result = GenerateRandomInteger(playerNames.Count);
            }

            return playerNames[result];
        }

        private static DateTime GetUniqueDate(List<EnterGameResultsCommand> results)
        {
            var result = GenerateRandomDate();

            while (results.Any(x => x.GameDate.Year == result.Year && x.GameDate.Month == result.Month && x.GameDate.Day == result.Day))
            {
                result = GenerateRandomDate();
            }

            return result;
        }

        private static DateTime GenerateRandomDate()
        {
            var minYear = 1950;
            var maxYear = 2016;
            var randomDays = GenerateRandomInteger((maxYear - minYear) * 365);

            var result = new DateTime(minYear, 1, 1);

            return result.AddDays(randomDays);
        }

        private static int GenerateRandomInteger(int max)
        {
            var rnd = new Random();
            return rnd.Next(max);
        }
    }
}
