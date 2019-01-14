using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class GenerateSampleData
    {
        private static Random _rnd = new Random();

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

        private static IEnumerable<ICommand> GenerateSampleDataCommands(int numberOfGames)
        {
            var results = new List<ICommand>();
            var samplePlayers = GenerateSamplePlayers();

            results.AddRange(samplePlayers);

            for (int g = 0; g < numberOfGames; g++)
            {
                var newGame = new CreateGameCommand();
                newGame.GameId = Guid.NewGuid();
                newGame.GameDate = GetUniqueDate(results);
                results.Add(newGame);

                var numPlayers = _rnd.Next(5, 15);
                var players = new List<AddPlayerToGameCommand>();

                for (int p = 0; p < numPlayers; p++)
                {
                    var newPlayer = new AddPlayerToGameCommand();
                    newPlayer.PlayerId = GetRandomPlayer(samplePlayers, players);
                    newPlayer.GameId = newGame.GameId;

                    // TODO: Add random amount of rebuys, and knock player out
                    players.Add(newPlayer);
                }

                results.AddRange(players);
            }

            return results;
        }

        private static IEnumerable<CreatePlayerCommand> GenerateSamplePlayers()
        {
            yield return new CreatePlayerCommand() { PlayerName = "Dylan Smith" };
            yield return new CreatePlayerCommand() { PlayerName = "Ryan Fritsch" };
            yield return new CreatePlayerCommand() { PlayerName = "Sauce" };
            yield return new CreatePlayerCommand() { PlayerName = "Shane Wilkins" };
            yield return new CreatePlayerCommand() { PlayerName = "G.W. Stein" };
            yield return new CreatePlayerCommand() { PlayerName = "Colin Hickson" };
            yield return new CreatePlayerCommand() { PlayerName = "Grant Hirose" };
            yield return new CreatePlayerCommand() { PlayerName = "Jeff" };
            yield return new CreatePlayerCommand() { PlayerName = "Alex K" };
            yield return new CreatePlayerCommand() { PlayerName = "Rob Schneider" };
            yield return new CreatePlayerCommand() { PlayerName = "Sean Kehoe" };
            yield return new CreatePlayerCommand() { PlayerName = "Meghan Mawhinney" };
            yield return new CreatePlayerCommand() { PlayerName = "Ray Tara" };
            yield return new CreatePlayerCommand() { PlayerName = "Sam Pearce" };
            yield return new CreatePlayerCommand() { PlayerName = "Jason The" };
            yield return new CreatePlayerCommand() { PlayerName = "Chris Wentz" };
            yield return new CreatePlayerCommand() { PlayerName = "Kiana Lindsay" };
            yield return new CreatePlayerCommand() { PlayerName = "Sherika Vollmer" };
            yield return new CreatePlayerCommand() { PlayerName = "Alfred Rolando" };
            yield return new CreatePlayerCommand() { PlayerName = "Chauncey Cavallaro" };
            yield return new CreatePlayerCommand() { PlayerName = "Karl Brush" };
            yield return new CreatePlayerCommand() { PlayerName = "Carlos Brumett" };
            yield return new CreatePlayerCommand() { PlayerName = "Hwa Gensler" };
            yield return new CreatePlayerCommand() { PlayerName = "Lynnette Levan" };
            yield return new CreatePlayerCommand() { PlayerName = "Jovita Tongue" };
            yield return new CreatePlayerCommand() { PlayerName = "Alyse Mauk" };
            yield return new CreatePlayerCommand() { PlayerName = "Sanjuanita Zieman" };
            yield return new CreatePlayerCommand() { PlayerName = "Glory Vanwagenen" };
            yield return new CreatePlayerCommand() { PlayerName = "Betsy Vasques" };
            yield return new CreatePlayerCommand() { PlayerName = "Elouise Allison" };
            yield return new CreatePlayerCommand() { PlayerName = "Sheridan Oxner" };
            yield return new CreatePlayerCommand() { PlayerName = "Sunni Cooke" };
            yield return new CreatePlayerCommand() { PlayerName = "Ozell Funston" };
            yield return new CreatePlayerCommand() { PlayerName = "Dorotha Winland" };
            yield return new CreatePlayerCommand() { PlayerName = "Estelle Weibel" };
            yield return new CreatePlayerCommand() { PlayerName = "Corazon Benware" };
            yield return new CreatePlayerCommand() { PlayerName = "Mabelle Bopp" };
            yield return new CreatePlayerCommand() { PlayerName = "Hope Byfield" };
            yield return new CreatePlayerCommand() { PlayerName = "Romeo Winters" };
            yield return new CreatePlayerCommand() { PlayerName = "Sasha Mongeau" };
            yield return new CreatePlayerCommand() { PlayerName = "Ricki Westendorf" };
            yield return new CreatePlayerCommand() { PlayerName = "Lane Clink" };
            yield return new CreatePlayerCommand() { PlayerName = "Max Chesnutt" };
            yield return new CreatePlayerCommand() { PlayerName = "Demetrius Reighard" };
            yield return new CreatePlayerCommand() { PlayerName = "Suzanna Basel" };
            yield return new CreatePlayerCommand() { PlayerName = "Claud Caverly" };
        }

        private static Guid GetRandomPlayer(IEnumerable<CreatePlayerCommand> allPlayers, IEnumerable<AddPlayerToGameCommand> players)
        {
            var result = allPlayers.ElementAt(GenerateRandomInteger(allPlayers.Count())).PlayerId;

            while (players.Any(p => p.PlayerId == result))
            {
                result = allPlayers.ElementAt(GenerateRandomInteger(allPlayers.Count())).PlayerId;
            }

            return result;
        }

        private static DateTime GetUniqueDate(List<ICommand> results)
        {
            var result = GenerateRandomDate();

            while (results.Any(x => x is CreateGameCommand g && DoesDateMatch(g, result)))
            {
                result = GenerateRandomDate();
            }

            return result;
        }

        private static bool DoesDateMatch(CreateGameCommand x, DateTime result)
        {
            return x.GameDate.Year == result.Year && x.GameDate.Month == result.Month && x.GameDate.Day == result.Day;
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
            return _rnd.Next(max);
        }
    }
}
