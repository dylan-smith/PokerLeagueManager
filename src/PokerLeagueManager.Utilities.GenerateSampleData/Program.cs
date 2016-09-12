using System;
using System.Collections.Generic;
using System.ServiceModel;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities.GenerateSampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Expected 1 argument", "args");
            }

            var serviceUrl = args[0];

            using (var svc = new CommandServiceProxy())
            {
                svc.Endpoint.Address = new EndpointAddress(serviceUrl);

                foreach (var cmd in GetSampleDataCommands())
                {
                    svc.ExecuteCommand(cmd);
                }
            }
        }

        private static IEnumerable<ICommand> GetSampleDataCommands()
        {
            var results = new List<ICommand>();

            // Game 1 - 7 players
            var game1 = new EnterGameResultsCommand();
            game1.GameDate = DateTime.Parse("07-Jul-2016");

            var players1 = new List<EnterGameResultsCommand.GamePlayer>();
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Dylan Smith", Placing = 1, Winnings = 160, PayIn = 50 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Ryan Fritsch", Placing = 2, Winnings = 70, PayIn = 20 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Shane Wilkins", Placing = 3, Winnings = 20, PayIn = 70 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Ray Tara", Placing = 4, Winnings = 0, PayIn = 30 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Sean Kehoe", Placing = 5, Winnings = 0, PayIn = 20 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "GW Stein", Placing = 6, Winnings = 0, PayIn = 20 });
            players1.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Colin Hickson", Placing = 7, Winnings = 0, PayIn = 40 });

            game1.Players = players1;

            results.Add(game1);

            // Game 2 - 5 Players
            var game2 = new EnterGameResultsCommand();
            game2.GameDate = DateTime.Parse("14-Jul-2016");

            var players2 = new List<EnterGameResultsCommand.GamePlayer>();
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Shane Wilkins", Placing = 1, Winnings = 100, PayIn = 30 });
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Ryan Fritsch", Placing = 2, Winnings = 50, PayIn = 20 });
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Tim Saunders", Placing = 3, Winnings = 0, PayIn = 20 });
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Jeff DS", Placing = 4, Winnings = 0, PayIn = 30 });
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Meghan Kehoe", Placing = 5, Winnings = 0, PayIn = 40 });

            game2.Players = players2;

            results.Add(game2);

            // Game 3 - 3 Players
            var game3 = new EnterGameResultsCommand();
            game3.GameDate = DateTime.Parse("21-Jul-2016");

            var players3 = new List<EnterGameResultsCommand.GamePlayer>();
            players3.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Sauce", Placing = 1, Winnings = 70, PayIn = 30 });
            players3.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Dylan Smith", Placing = 2, Winnings = 10, PayIn = 40 });
            players3.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Ryan Fritsch", Placing = 3, Winnings = 0, PayIn = 10 });

            game3.Players = players3;

            results.Add(game3);

            return results;
        }
    }
}
