using System;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class CreateGameCommandHandler : BaseCommandHandler, IHandlesCommand<CreateGameCommand>
    {
        public void Execute(CreateGameCommand command)
        {
            if (Repository.DoesAggregateExist(command.GameId))
            {
                throw new ArgumentException("Cannot enter game results for a previously existing Game Id", "GameId");
            }

            var gameCount = QueryService.Execute(new GetGameCountByDateQuery(command.GameDate));

            if (gameCount > 0)
            {
                throw new ArgumentException("Cannot enter multiple game results for the same date", "GameDate");
            }

            var game = new Game(command.GameId, command.GameDate);

            Repository.PublishEvents(game, command);
        }
    }
}
