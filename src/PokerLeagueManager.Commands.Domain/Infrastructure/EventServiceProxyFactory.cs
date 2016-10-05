using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxyFactory : IEventServiceProxyFactory
    {
        private IEventServiceProxy _proxy;

        public EventServiceProxyFactory(IEventServiceProxy proxy)
        {
            _proxy = proxy;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "EventServiceProxy doesn't have a Dispose method")]
        public IEventServiceProxy Create(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (_proxy.ServiceUrl != (string)row["SubscriberUrl"])
            {
                _proxy.ServiceUrl = (string)row["SubscriberUrl"];
            }

            return _proxy;
        }
    }
}
