using System;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class ChangeGameDateCommandHandler : BaseCommandHandler, IHandlesCommand<ChangeGameDateCommand>
    {
        public void Execute(ChangeGameDateCommand command)
        {
            var gameCount = QueryService.Execute(new GetGameCountByDateQuery(command.GameDate));

            if (gameCount > 0)
            {
                throw new ArgumentException("There is already a game created for this date. Cannot create more than one game for the same date", nameof(command));
            }

            var game = Repository.GetAggregateById<Game>(command.GameId);

            game.ChangeDate(command.GameDate);

            Repository.PublishEvents(game, command);
        }
    }
}
