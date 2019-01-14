using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.DeletePlayer
{
    [TestClass]
    public class DeletePlayerTest : BaseCommandTest
    {
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
        }

        [TestMethod]
        public void DeletePlayer()
        {
            RunTest(new DeletePlayerCommand() { PlayerId = _playerId });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerDeletedEvent() { PlayerId = _playerId };
        }
    }
}
