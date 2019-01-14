using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class DeletePlayerCommandHandler : BaseCommandHandler, IHandlesCommand<DeletePlayerCommand>
    {
        public void Execute(DeletePlayerCommand command)
        {
            var player = Repository.GetAggregateById<Player>(command.PlayerId);

            player.DeletePlayer();

            Repository.PublishEvents(player, command);
        }
    }
}
