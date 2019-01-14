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
    public class CreatePlayerAfterDeletingTest : BaseCommandTest
    {
        private string _playerName = "Homer Simpson";
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = _playerName };
            yield return new PlayerDeletedEvent() { PlayerId = _playerId };
        }

        [TestMethod]
        public void CreatePlayerAfterDeleting()
        {
            RunTest(new CreatePlayerCommand() { PlayerName = _playerName });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerCreatedEvent() { PlayerId = AnyGuid(), PlayerName = _playerName };
        }
    }
}
