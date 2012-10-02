using PokerLeagueManager.Commands.Domain.Aggregates.Game;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.CommandHandlers
{
    public class EnterGameResultsHandler : BaseCommandHandler, IHandlesCommand<EnterGameResultsCommand>
    {
        public void Execute(EnterGameResultsCommand command)
        {
            if (this.Repository.DoesAggregateExist(command.GameId))
            {
                throw new ArgumentException("Cannot enter game results for a previously existing Game Id", "GameId");
            }

            var game = new Game(command.GameId, command.GameDate);

            if (command.Players != null)
            {
                foreach (var player in command.Players)
                {
                    game.AddPlayer(player.PlayerName, player.Placing, player.Winnings);
                }
            }

            game.ValidateGame();

            this.Repository.PublishEvents(game, command);
        }
    }
}
