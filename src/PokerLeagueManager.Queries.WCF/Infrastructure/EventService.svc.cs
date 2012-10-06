using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PokerLeagueManager.Queries.WCF.Infrastructure
{
    public class EventService : IEventService
    {
        public void HandleEvent(IEvent e)
        {
            var eventHandlerFactory = Resolver.Container.Resolve<IEventHandlerFactory>();

            eventHandlerFactory.HandleEvent(e);
        }
    }
}
