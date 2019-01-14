using System;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class CreatePlayerCommandHandler : BaseCommandHandler, IHandlesCommand<CreatePlayerCommand>
    {
        public void Execute(CreatePlayerCommand command)
        {
            if (Repository.DoesAggregateExist(command.PlayerId))
            {
                throw new ArgumentException("Cannot create a duplicate Player Id", "PlayerId");
            }

            var playerCount = QueryService.Execute(new GetPlayerCountByNameQuery(command.PlayerName));

            if (playerCount > 0)
            {
                throw new ArgumentException("Player name must be unique", "PlayerName");
            }

            var player = new Player(command.PlayerId, command.PlayerName);

            Repository.PublishEvents(player, command);
        }
    }
}
