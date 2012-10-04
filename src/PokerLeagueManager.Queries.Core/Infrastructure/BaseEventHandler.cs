using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class BaseEventHandler
    {
        public IQueryDataStore QueryDataStore { get; set; }
    }
}
