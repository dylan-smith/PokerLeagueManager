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
    public class EnterGameResults_NoPlayersTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        [TestMethod]
        public void EnterGameResults_NoPlayers()
        {
            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameCreatedEvent() { GameDate = _gameDate };
        }
    }
}
