using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxyFactory : IEventServiceProxyFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "I'm willing to accept the potential resource leak if the prop assignment throws an exception")]
        public IEventServiceProxy Create(string url)
        {
            return new EventServiceProxy() { ServiceUrl = url };
        }
    }
}
