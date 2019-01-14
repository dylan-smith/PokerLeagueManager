using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxyFactory : IEventServiceProxyFactory
    {
        private readonly IDictionary<string, IEventServiceProxy> _proxies;

        public EventServiceProxyFactory()
        {
            _proxies = new Dictionary<string, IEventServiceProxy>();
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "EventServiceProxy doesn't have a Dispose method")]
        public IEventServiceProxy Create(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            var subscriberUrl = (string)row["SubscriberUrl"];

            if (_proxies.ContainsKey(subscriberUrl))
            {
                return _proxies[subscriberUrl];
            }
            else
            {
                var proxy = new EventServiceProxy();
                proxy.ServiceUrl = subscriberUrl;
                _proxies.Add(subscriberUrl, proxy);
                return proxy;
            }
        }
    }
}
