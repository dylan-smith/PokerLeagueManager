using System;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriber
    {
        private IEventService _eventService;

        public EventSubscriber(IEventService eventService)
        {
            _eventService = eventService;
        }

        public Guid SubscriberId { get; set; }

        public string SubscriberUrl { get; set; }

        public void Publish(IEvent e)
        {
            _eventService.HandleEvent(e);
        }
    }
}
