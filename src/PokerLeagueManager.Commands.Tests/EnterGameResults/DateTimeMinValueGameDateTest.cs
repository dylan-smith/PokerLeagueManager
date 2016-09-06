using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Tests.EnterGameResults
{
    [TestClass]
    public class DateTimeMinValueGameDateTest : BaseCommandTest
    {
        private DateTime _gameDate = DateTime.MinValue;

        [TestMethod]
        public void DateTimeMinValueGameDate()
        {
            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate });
        }

        public override Exception ExpectedException()
        {
            return new InvalidGameDateException(_gameDate);
        }
    }
}
