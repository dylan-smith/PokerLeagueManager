﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.AddPlayerToGame
{
    [TestClass]
    public class RemovePlayerFromGameInvalidPlayerTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = Guid.NewGuid() };
        }

        [TestMethod]
        public void RemovePlayerFromGame()
        {
            RunTest(new RemovePlayerFromGameCommand() { GameId = _gameId, PlayerId = _playerId });
        }

        public override Exception ExpectedException()
        {
            return new PlayerNotInGameException(_playerId, _gameId);
        }
    }
}
