using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class RemoveRebuyCommandHandler : BaseCommandHandler, IHandlesCommand<RemoveRebuyCommand>
    {
        public void Execute(RemoveRebuyCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.RemoveRebuy(command.PlayerId);

            Repository.PublishEvents(game, command);
        }
    }
}
