﻿using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.WCF
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
