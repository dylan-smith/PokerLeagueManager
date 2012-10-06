using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PokerLeagueManager.Queries.WCF.Infrastructure
{
    [ServiceContract]
    public interface IEventService
    {
        [OperationContract]
        void HandleEvent(IEvent e);
    }
}
