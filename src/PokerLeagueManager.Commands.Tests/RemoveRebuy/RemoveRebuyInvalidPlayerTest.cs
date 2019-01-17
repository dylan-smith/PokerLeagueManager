using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.RemoveRebuy
{
    [TestClass]
    public class RemoveRebuyInvalidPlayerTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId };
            yield return new RebuyAddedEvent() { GameId = _gameId, PlayerId = _playerId };
        }

        [TestMethod]
        public void RemoveRebuyInvalidPlayer()
        {
            RunTest(new RemoveRebuyCommand() { GameId = _gameId, PlayerId = Guid.NewGuid() });
        }

        public override Exception ExpectedException()
        {
            return new PlayerNotInGameException(AnyGuid(), _gameId);
        }
    }
}
