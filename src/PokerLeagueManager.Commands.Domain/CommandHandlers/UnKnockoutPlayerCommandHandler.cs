using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class UnKnockoutPlayerCommandHandler : BaseCommandHandler, IHandlesCommand<UnKnockoutPlayerCommand>
    {
        public void Execute(UnKnockoutPlayerCommand command)
        {
            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.UnKnockoutPlayer(command.PlayerId);

            Repository.PublishEvents(game, command);
        }
    }
}
