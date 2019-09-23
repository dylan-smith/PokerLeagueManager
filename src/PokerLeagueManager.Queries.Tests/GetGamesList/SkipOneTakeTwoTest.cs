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
    public class SkipOneTakeTwoTest : BaseQueryTest
    {
        private readonly Guid _gameId1 = Guid.NewGuid();
        private readonly Guid _gameId2 = Guid.NewGuid();
        private readonly Guid _gameId3 = Guid.NewGuid();
        private readonly Guid _gameId4 = Guid.NewGuid();

        private readonly DateTime _gameDate1 = DateTime.Parse("03-Jul-1981");
        private readonly DateTime _gameDate2 = DateTime.Parse("03-Jul-2015");
        private readonly DateTime _gameDate3 = DateTime.Parse("03-Jul-2016");
        private readonly DateTime _gameDate4 = DateTime.Parse("03-Jul-2017");

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = _gameId1, GameDate = _gameDate1 };
            yield return new GameCreatedEvent() { AggregateId = _gameId2, GameDate = _gameDate2 };
            yield return new GameCreatedEvent() { AggregateId = _gameId3, GameDate = _gameDate3 };
            yield return new GameCreatedEvent() { AggregateId = _gameId4, GameDate = _gameDate4 };
        }

        [TestMethod]
        public void GetGamesList_SkipOneTakeTwo()
        {
            var querySvc = SetupQueryService();

            var result = querySvc.Execute(new GetGamesListQuery(1, 2));

            Assert.AreEqual(2, result.Count());
            Assert.IsNotNull(result.Single(x => x.GameId == _gameId2));
            Assert.IsNotNull(result.Single(x => x.GameId == _gameId3));
        }
    }
}
