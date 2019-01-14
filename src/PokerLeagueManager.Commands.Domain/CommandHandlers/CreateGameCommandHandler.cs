using System;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
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
                throw new ArgumentException("Cannot create a duplicate Game Id", "GameId");
            }

            var gameCount = QueryService.Execute(new GetGameCountByDateQuery(command.GameDate));

            if (gameCount > 0)
            {
                throw new ArgumentException("Cannot create more than one game for the same date", "GameDate");
            }

            var game = new Game(command.GameId, command.GameDate);

            Repository.PublishEvents(game, command);
        }
    }
}
