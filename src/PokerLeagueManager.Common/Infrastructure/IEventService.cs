using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(EventTypeProvider))]
    public interface IEventService
    {
        [OperationContract]
        void HandleEvent(IEvent e);
    }
}
