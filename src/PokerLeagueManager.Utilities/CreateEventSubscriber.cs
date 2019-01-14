using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities
{
    public static class CreateEventSubscriber
    {
        public static void Create(string[] args)
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
    }
}
