﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.PayoutsCalculated
{
    [TestClass]
    public class PayoutsCalculatedOnePlayerTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
        }

        [TestMethod]
        public void PayoutsCalculatedOnePlayer()
        {
            RunTest(new AddPlayerToGameCommand() { GameId = _gameId, PlayerId = _playerId });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId, BuyinAmount = 20 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 10, Second = 0, Third = 0 };
        }
    }
}
