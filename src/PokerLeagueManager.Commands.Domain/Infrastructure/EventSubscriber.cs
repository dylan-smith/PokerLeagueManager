using PokerLeagueManager.Common.Events.Infrastructure;
using System;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriber
    {
        public Guid SubscriberId { get; set; }
        public string SubscriberUrl { get; set; }

        private IEventService _eventService;

        public EventSubscriber(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void Publish(IEvent e)
        {
            _eventService.HandleEvent(e);
        }
    }
}
