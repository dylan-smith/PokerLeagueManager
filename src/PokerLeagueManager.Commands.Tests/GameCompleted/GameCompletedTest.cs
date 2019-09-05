using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.GameCompleted
{
    [TestClass]
    public class GameCompletedTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId1 = Guid.NewGuid();
        private Guid _playerId2 = Guid.NewGuid();
        private Guid _playerId3 = Guid.NewGuid();
        private Guid _playerId4 = Guid.NewGuid();
        private Guid _playerId5 = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId1, PlayerName = "Homer Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId2, PlayerName = "Bart Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId3, PlayerName = "Rick Sanchez" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId4, PlayerName = "Jon Snow" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId5, PlayerName = "Tyrion Lannister" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId1 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId2 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId4 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId5 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId5 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId4 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId3 };
        }

        [TestMethod]
        public void GameCompleted()
        {
            RunTest(new KnockoutPlayerCommand() { GameId = _gameId, PlayerId = _playerId2 });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId2 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId1 };

            var completedEvent = new GameCompletedEvent() { GameId = _gameId, First = 60, Second = 30, Third = 0 };

            completedEvent.Placings.Add(_playerId1, 1);
            completedEvent.Placings.Add(_playerId2, 2);
            completedEvent.Placings.Add(_playerId3, 3);
            completedEvent.Placings.Add(_playerId4, 4);
            completedEvent.Placings.Add(_playerId5, 5);

            yield return completedEvent;
        }
    }
}
