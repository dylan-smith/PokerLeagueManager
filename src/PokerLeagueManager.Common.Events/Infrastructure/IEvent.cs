using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Events.Infrastructure
{
    public interface IEvent
    {
        // TODO: Remove User from the event, if we have a command linked we already have that in the command
        Guid EventId { get; set; }

        DateTime Timestamp { get; set; }

        string User { get; set; }

        Guid CommandId { get; set; }

        Guid AggregateId { get; set; }
    }
}
