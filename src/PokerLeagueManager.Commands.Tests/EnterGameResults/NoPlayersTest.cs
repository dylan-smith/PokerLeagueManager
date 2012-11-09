﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Events.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.EnterGameResults
{
    [TestClass]
    public class NoPlayersTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        [TestMethod]
        public void NoPlayers()
        {
            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameCreatedEvent() { AggregateId = AnyGuid(), GameDate = _gameDate };
        }
    }
}