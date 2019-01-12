using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.CreateGame
{
    [TestClass]
    public class CreateGameWithDuplicateDateTest : BaseCommandTest
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = Guid.NewGuid(), GameDate = _gameDate };
        }

        [TestMethod]
        public void CreateGameWithDuplicateDate()
        {
            RunTest(new CreateGameCommand() { GameDate = _gameDate.AddHours(2) });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
