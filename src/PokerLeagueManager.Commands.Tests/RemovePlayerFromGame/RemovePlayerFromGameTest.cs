using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.AddPlayerToGame
{
    [TestClass]
    public class RemovePlayerFromGameTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId };
        }

        [TestMethod]
        public void RemovePlayerFromGame()
        {
            RunTest(new RemovePlayerFromGameCommand() { GameId = _gameId, PlayerId = _playerId });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerRemovedFromGameEvent() { GameId = _gameId, PlayerId = _playerId };
            yield return new PayoutsCalculatedEvent() { GameId = _gameId, First = AnyInt(), Second = AnyInt(), Third = AnyInt() };
        }
    }
}
