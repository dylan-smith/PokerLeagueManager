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
    public class CreatePlayerWithDuplicatePlayerIdTest : BaseCommandTest
    {
        private string _playerName = "Homer Simpson";
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Simon Timms" };
        }

        [TestMethod]
        public void CreateGameWithDuplicateGameId()
        {
            RunTest(new CreatePlayerCommand() { PlayerId = _playerId, PlayerName = _playerName });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
