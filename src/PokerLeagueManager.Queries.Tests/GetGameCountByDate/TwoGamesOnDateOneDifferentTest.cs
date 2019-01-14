﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests.GetGameCountByDate
{
    [TestClass]
    public class TwoGamesOnDateOneDifferentTest : BaseQueryTest
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { AggregateId = Guid.NewGuid(), GameDate = _gameDate };
            yield return new GameCreatedEvent() { AggregateId = Guid.NewGuid(), GameDate = _gameDate.AddDays(1) };
            yield return new GameCreatedEvent() { AggregateId = Guid.NewGuid(), GameDate = _gameDate };
        }

        [TestMethod]
        public void GetGameCountByDate_TwoGamesOnDateOneDifferent()
        {
            var result = SetupQueryService().Execute(new GetGameCountByDateQuery(_gameDate));

            Assert.AreEqual(2, result);
        }
    }
}
