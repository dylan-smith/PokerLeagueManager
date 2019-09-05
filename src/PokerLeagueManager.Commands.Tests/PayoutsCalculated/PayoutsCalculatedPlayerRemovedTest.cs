using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.PayoutsCalculated
{
    [TestClass]
    public class PayoutsCalculatedPlayerRemovedTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId1 = Guid.NewGuid();
        private Guid _playerId2 = Guid.NewGuid();
        private Guid _playerId3 = Guid.NewGuid();
        private Guid _playerId4 = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId1, PlayerName = "Homer Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId2, PlayerName = "Bart Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId3, PlayerName = "Rick Sanchez" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId4, PlayerName = "Jon Snow" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId1 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId2 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId4 };
        }

        [TestMethod]
        public void PayoutsCalculatedPlayerRemoved()
        {
            RunTest(new RemovePlayerFromGameCommand() { GameId = _gameId, PlayerId = _playerId3 });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerRemovedFromGameEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = 30, Second = 20, Third = 0 };
        }
    }
}
