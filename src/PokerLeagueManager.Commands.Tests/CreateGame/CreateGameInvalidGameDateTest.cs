using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Tests.CreateGame
{
    [TestClass]
    public class CreateGameInvalidGameDateTest : BaseCommandTest
    {
        private DateTime _gameDate = DateTime.MaxValue;

        [TestMethod]
        public void CreateGameInvalidGameDate()
        {
            RunTest(new CreateGameCommand() { GameDate = _gameDate });
        }

        public override Exception ExpectedException()
        {
            return new InvalidGameDateException(_gameDate);
        }
    }
}
