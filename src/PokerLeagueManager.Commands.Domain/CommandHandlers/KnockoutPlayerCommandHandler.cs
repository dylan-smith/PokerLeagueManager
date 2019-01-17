using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class KnockoutPlayerCommandHandler : BaseCommandHandler, IHandlesCommand<KnockoutPlayerCommand>
    {
        public void Execute(KnockoutPlayerCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.KnockoutPlayer(command.PlayerId);

            Repository.PublishEvents(game, command);
        }
    }
}
