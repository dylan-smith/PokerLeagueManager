using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventSubscriberFactory
    {
        EventSubscriber Create(DataRow row);
    }
}
