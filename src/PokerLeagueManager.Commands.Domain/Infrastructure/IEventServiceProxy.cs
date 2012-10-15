using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface IEventServiceProxy : IEventService
    {
        string ServiceUrl { get; set; }
    }
}
