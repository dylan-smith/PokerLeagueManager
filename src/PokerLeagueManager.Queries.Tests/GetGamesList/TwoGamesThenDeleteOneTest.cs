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
    public class TwoGamesThenDeleteOneTest : BaseQueryTest
    {
        private readonly Guid _gameId1 = Guid.NewGuid();
        private readonly Guid _gameId2 = Guid.NewGuid();

        private readonly DateTime _gameDate1 = DateTime.Parse("03-Jul-1981");
        private readonly DateTime _gameDate2 = DateTime.Parse("03-Jul-2015");

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = _gameId1, GameDate = _gameDate1 };
            yield return new GameCreatedEvent() { AggregateId = _gameId2, GameDate = _gameDate2 };
            yield return new GameDeletedEvent() { AggregateId = _gameId1 };
        }

        [TestMethod]
        public void GetGamesList_TwoGamesThenDeleteOne()
        {
            var querySvc = SetupQueryService();

            var result = querySvc.Execute(new GetGamesListQuery());

            Assert.AreEqual(1, result.Count());
        }
    }
}
