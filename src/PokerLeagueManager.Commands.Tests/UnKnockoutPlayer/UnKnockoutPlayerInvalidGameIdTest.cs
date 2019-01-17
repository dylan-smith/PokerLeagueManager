using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.UnKnockoutPlayer
{
    [TestClass]
    public class UnKnockoutPlayerInvalidGameIdTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId };
        }

        [TestMethod]
        public void UnKnockoutPlayerInvalidGameId()
        {
            RunTest(new UnKnockoutPlayerCommand() { GameId = Guid.NewGuid(), PlayerId = _playerId });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
