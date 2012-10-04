using PokerLeagueManager.Commands.Domain.QueryServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class BaseCommandHandler
    {
        public IEventRepository Repository { get; set; }
        public IQueryService QueryService { get; set; }
    }
}
