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
    public class CreatePlayerWithPlayerIdTest : BaseCommandTest
    {
        private string _playerName = "Homer Simpson";
        private Guid _playerId = Guid.NewGuid();

        [TestMethod]
        public void CreatePlayerWithPlayerId()
        {
            RunTest(new CreatePlayerCommand() { PlayerId = _playerId, PlayerName = _playerName });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = _playerName };
        }
    }
}
