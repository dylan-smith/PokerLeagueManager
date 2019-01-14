using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class RemovePlayerFromGameCommandHandler : BaseCommandHandler, IHandlesCommand<RemovePlayerFromGameCommand>
    {
        public void Execute(RemovePlayerFromGameCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.RemovePlayerFromGame(command.PlayerId);

            Repository.PublishEvents(game, command);
        }
    }
}
