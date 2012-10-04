using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using System.Collections.Generic;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Commands.Domain.QueryServiceReference;
using Moq;

namespace PokerLeagueManager.Commands.Tests
{
    [TestClass]
    public class EnterGameResults_TwoGamesWithSameDateTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        public override IQueryService GivenQueryResults()
        {
            var mockQueryService = new Mock<IQueryService>();
            mockQueryService.Setup(x => x.GetGameCountByDate(_gameDate)).Returns(1);
            return mockQueryService.Object;
        }

        [TestMethod]
        public void EnterGameResults_TwoGamesWithSameDate()
        {
            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
