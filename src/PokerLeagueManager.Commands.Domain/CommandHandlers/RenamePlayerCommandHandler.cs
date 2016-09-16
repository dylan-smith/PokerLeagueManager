using System.Linq;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class RenamePlayerCommandHandler : BaseCommandHandler, IHandlesCommand<RenamePlayerCommand>
    {
        public void Execute(RenamePlayerCommand command)
        {
            var games = QueryService.Execute(new GetGamesWithPlayerQuery(command.OldPlayerName));
            var aggregates = games.Select(g => Repository.GetAggregateById<Game>(g.GameId)).ToList();

            foreach (var g in aggregates)
            {
                g.RenamePlayer(command.OldPlayerName, command.NewPlayerName);
            }

            Repository.PublishEvents(aggregates, command);
        }
    }
}
