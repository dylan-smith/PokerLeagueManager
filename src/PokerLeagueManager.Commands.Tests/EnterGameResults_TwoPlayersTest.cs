using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using System.Collections.Generic;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;

namespace PokerLeagueManager.Commands.Tests
{
    [TestClass]
    public class EnterGameResults_TwoPlayersTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        [TestMethod]
        public void EnterGameResults_TwoPlayers()
        {
            var players = new List<EnterGameResultsCommand.GamePlayer>();
            players.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Dylan Smith", Placing = 1, Winnings = 100 });
            players.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Grant Hirose", Placing = 2, Winnings = 50 });

            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate, Players = players });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameCreatedEvent() { GameDate = _gameDate };
            yield return new PlayerAddedToGameEvent() { PlayerName = "Dylan Smith", Placing = 1, Winnings = 100 };
            yield return new PlayerAddedToGameEvent() { PlayerName = "Grant Hirose", Placing = 2, Winnings = 50 };
        }
    }
}
