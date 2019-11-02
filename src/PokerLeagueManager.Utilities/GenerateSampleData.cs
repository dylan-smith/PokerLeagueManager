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
            var samplePlayers = GenerateSamplePlayers().ToList();

            results.AddRange(samplePlayers);

            for (int g = 0; g < numberOfGames; g++)
            {
                var newGame = new CreateGameCommand();
                newGame.GameId = Guid.NewGuid();
                newGame.GameDate = GetUniqueDate(results);
                results.Add(newGame);

                var numPlayers = _rnd.Next(5, 10);
                var players = new List<AddPlayerToGameCommand>();

                for (int p = 0; p < numPlayers; p++)
                {
                    var newPlayer = new AddPlayerToGameCommand();
                    newPlayer.PlayerId = GetRandomPlayer(samplePlayers, players);
                    newPlayer.GameId = newGame.GameId;

                    players.Add(newPlayer);
                }

                results.AddRange(players);

                for (int p = 1; p < numPlayers; p++)
                {
                    var knockoutCommand = new KnockoutPlayerCommand();
                    knockoutCommand.GameId = newGame.GameId;
                    knockoutCommand.PlayerId = players[p].PlayerId;

                    results.Add(knockoutCommand);
                }
            }

            return results;
        }

        private static IEnumerable<CreatePlayerCommand> GenerateSamplePlayers()
        {
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Dylan Smith" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Ryan Fritsch" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Sauce" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Shane Wilkins" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "G.W. Stein" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Colin Hickson" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Grant Hirose" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Jeff" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Alex K" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Rob Schneider" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Sean Kehoe" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Meghan Mawhinney" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Ray Tara" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Sam Pearce" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Jason The" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Chris Wentz" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Kiana Lindsay" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Sherika Vollmer" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Alfred Rolando" };
            yield return new CreatePlayerCommand() { PlayerId = Guid.NewGuid(), PlayerName = "Chauncey Cavallaro" };
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
