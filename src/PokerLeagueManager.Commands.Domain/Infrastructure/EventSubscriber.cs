using PokerLeagueManager.Common.Events.Infrastructure;
using System;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventSubscriber
    {
        // TODO: Do we even need an EventSubscriber right now, can't the repository just have a col of EventServices
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
