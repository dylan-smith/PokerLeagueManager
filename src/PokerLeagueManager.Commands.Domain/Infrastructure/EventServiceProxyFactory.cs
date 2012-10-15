using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxyFactory : IEventServiceProxyFactory
    {
        public IEventServiceProxy Create(string url)
        {
            return new EventServiceProxy() { ServiceUrl = url };
        }
    }
}
