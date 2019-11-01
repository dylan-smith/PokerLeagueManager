using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests.GetGamesList
{
    [TestClass]
    public class OneGameWithDetailsTest : BaseQueryTest
    {
        private readonly Guid _gameId = Guid.NewGuid();
        private readonly DateTime _gameDate = DateTime.Parse("03-Jul-1981");
        private readonly string _winner = "Dylan Smith";
        private readonly Guid _playerId1 = Guid.NewGuid();
        private readonly Guid _playerId2 = Guid.NewGuid();
        private readonly Guid _playerId3 = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = _gameId, GameDate = _gameDate };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId1, PlayerName = _winner };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId2, PlayerName = "Bart Simpson" };
            yield return new PlayerCreatedEvent() { PlayerId = _playerId3, PlayerName = "Rick Sanchez" };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId1 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId2 };
            yield return new PlayerAddedToGameEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId3 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId2 };
            yield return new PlayerKnockedOutEvent() { GameId = _gameId, PlayerId = _playerId1 };

            var completedEvent = new GameCompletedEvent() { GameId = _gameId, First = 30 };
            completedEvent.Placings.Add(_playerId1, 1);
            completedEvent.Placings.Add(_playerId2, 2);
            completedEvent.Placings.Add(_playerId3, 3);
            yield return completedEvent;
        }

        [TestMethod]
        public void GetGamesList_TwoGames()
        {
            var result = SetupQueryService().Execute(new GetGamesListQuery());

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(_winner, result.First().Winner);
            Assert.AreEqual(30, result.First().Winnings);
            Assert.AreEqual(_gameId, result.First().GameId);
            Assert.AreEqual(_gameDate, result.First().GameDate);
        }
    }
}
