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
            var targetDatabaseServer = args[0];
            var subscriberUrl = args[1];

            using (var db = new SqlServerDatabaseLayer())
            {
                db.ConnectionString = $"Data Source={targetDatabaseServer};Initial Catalog=PokerLeagueManager.DB.EventStore;Integrated Security=True;Pooling=False";
                db.ExecuteNonQuery($"INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), '{subscriberUrl}')");
            }
        }
    }
}
