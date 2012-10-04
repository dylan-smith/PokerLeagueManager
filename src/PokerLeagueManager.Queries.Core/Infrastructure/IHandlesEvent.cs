using PokerLeagueManager.Common.Events.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public interface IHandlesEvent<T> where T : IEvent
    {
        void Handle(T e);
    }
}
