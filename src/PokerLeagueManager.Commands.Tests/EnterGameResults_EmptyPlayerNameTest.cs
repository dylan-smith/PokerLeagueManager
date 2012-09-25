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
    public class EnterGameResults_EmptyPlayerNameTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        [TestMethod]
        public void EnterGameResults_EmptyPlayerName()
        {
            var players = new List<EnterGameResultsCommand.GamePlayer>();
            players.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "", Placing = 1, Winnings = 100 });

            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate, Players = players });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
