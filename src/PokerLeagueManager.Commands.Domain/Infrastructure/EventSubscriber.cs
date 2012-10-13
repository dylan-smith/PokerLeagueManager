using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriber
    {
        public Guid SubscriberId { get; set; }
        public string SubscriberUrl { get; set; }

        public void Publish(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
