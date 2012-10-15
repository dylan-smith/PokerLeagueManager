using PokerLeagueManager.Common.Events.Infrastructure;
using System.ServiceModel;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxy : ClientBase<IEventService>, IEventService, IEventServiceProxy
    {
        public void HandleEvent(IEvent e)
        {
            base.Channel.HandleEvent(e);
        }

        public string ServiceUrl
        {
            get
            {
                return base.Endpoint.Address.Uri.ToString();
            }
            set
            {
                base.Endpoint.Address = new EndpointAddress(value);
            }
        }
    }
}
