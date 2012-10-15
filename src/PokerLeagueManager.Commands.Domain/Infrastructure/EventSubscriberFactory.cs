using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriberFactory : IEventSubscriberFactory
    {
        private IEventServiceProxyFactory _eventServiceProxyFactory;

        public EventSubscriberFactory(IEventServiceProxyFactory eventServiceProxyFactory)
        {
            _eventServiceProxyFactory = eventServiceProxyFactory;
        }

        public EventSubscriber Create(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row", "row cannot be null");
            }

            var result = new EventSubscriber(_eventServiceProxyFactory.Create((string)row["SubscriberUrl"]));

            result.SubscriberId = (Guid)row["SubscriberId"];
            result.SubscriberUrl = (string)row["SubscriberUrl"];

            return result;
        }
    }
}
