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
    public class EnterGameResultsHandler : BaseCommandHandler, IHandlesCommand<EnterGameResults>
    {
        public void Execute(EnterGameResults command)
        {
            var game = new Game(command.GameDate);

            foreach (var player in command.Players)
            {
                game.AddPlayer(player.PlayerName, player.Placing, player.Winnings);
            }

            game.ValidateGame();

            this.Repository.PublishEvents(game.PendingEvents, command);
        }
    }
}
