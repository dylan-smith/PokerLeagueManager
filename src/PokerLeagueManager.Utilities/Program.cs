using System;
using System.Collections.Generic;
using System.ServiceModel;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var action = args[0];

            switch (action)
            {
                case "CreateEventSubscriber":
                    CreateEventSubscriber(args);
                    break;
                case "GenerateSampleData":
                    GenerateSampleData(args);
                    break;
                default:
                    throw new ArgumentException("Unrecognized Action");
            }
        }

        private static void GenerateSampleData(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Expected 2 arguments", "args");
            }

            var serviceUrl = args[1];

            Console.WriteLine($"serviceUrl: {serviceUrl}");

            using (var svc = new CommandServiceProxy())
            {
                svc.SetUrl(serviceUrl);

                foreach (var cmd in GetSampleDataCommands())
                {
                    Console.WriteLine($"Executing Command: {cmd.GetType().Name}");
                    svc.ExecuteCommand(cmd);
                }
            }
        }

        private static void CreateEventSubscriber(string[] args)
        {
            using (var db = new SqlServerDatabaseLayer())
            {
                var databaseServer = args[1];
                var database = args[2];

                if (args.Length > 4)
                {
                    var databaseUser = args[3];
                    var databasePassword = args[4];

                    db.ConnectionString = $"Data Source = {databaseServer}; Initial Catalog = {database}; User Id = {databaseUser}; Password = {databasePassword};";
                }
                else
                {
                    db.ConnectionString = $"Data Source = {databaseServer}; Initial Catalog = {database}; Integrated Security = True; Pooling = False";
                }

                var subscriberUrl = args[args.Length - 1];

                db.ExecuteNonQuery($"INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), '{subscriberUrl}')");
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
            players2.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Shane Wilkins", Placing = 1, Winnings = 90, PayIn = 30 });
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

            // Rename Shane to Shaner
            var renameCommand = new RenamePlayerCommand();
            renameCommand.OldPlayerName = "Shane Wilkins";
            renameCommand.NewPlayerName = "Shaner";
            results.Add(renameCommand);

            return results;
        }
    }
}
