using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class AddRebuyCommandHandler : BaseCommandHandler, IHandlesCommand<AddRebuyCommand>
    {
        public void Execute(AddRebuyCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.AddRebuy(command.PlayerId);

            Repository.PublishEvents(game, command);
        }
    }
}
