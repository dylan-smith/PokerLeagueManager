using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriberFactory : IEventSubscriberFactory
    {
        public EventSubscriber Create(System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
