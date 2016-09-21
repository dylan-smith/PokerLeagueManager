using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Utilities.CreateEventSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SqlServerDatabaseLayer())
            {
                var databaseServer = args[0];
                var database = args[1];

                if (args.Length > 3)
                {
                    var databaseUser = args[2];
                    var databasePassword = args[3];

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
