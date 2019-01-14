using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.CreatePlayer
{
    [TestClass]
    public class CreatePlayerWithDuplicateNameCaseInsensitiveTest : BaseCommandTest
    {
        private string _playerName = "Homer Simpson";

        public override IEnumerable<IEvent> Given()
        {
            yield return new PlayerCreatedEvent() { PlayerName = _playerName };
        }

        [TestMethod]
        public void CreatePlayerWithDuplicateNameCaseInsensitive()
        {
            RunTest(new CreatePlayerCommand() { PlayerName = _playerName.ToUpper() });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
