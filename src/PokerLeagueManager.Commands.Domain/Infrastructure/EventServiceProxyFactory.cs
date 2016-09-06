using System;
using System.Data;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxyFactory : IEventServiceProxyFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "EventServiceProxy doesn't have a Dispose method")]
        public IEventServiceProxy Create(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            return new EventServiceProxy() { ServiceUrl = (string)row["SubscriberUrl"] };
        }
    }
}
