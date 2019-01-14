using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class AddPlayerToGameCommandHandler : BaseCommandHandler, IHandlesCommand<AddPlayerToGameCommand>
    {
        public void Execute(AddPlayerToGameCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);
            var player = Repository.GetAggregateById<Player>(command.PlayerId);

            game.AddPlayerToGame(player);

            Repository.PublishEvents(game, command);
        }
    }
}
