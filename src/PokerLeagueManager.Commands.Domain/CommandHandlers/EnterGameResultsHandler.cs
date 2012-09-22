using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class EnterGameResultsHandler : BaseCommandHandler, IHandlesCommand<EnterGameResults>
    {
        public void Execute(EnterGameResults command)
        {
            throw new NotImplementedException();
        }
    }
}
