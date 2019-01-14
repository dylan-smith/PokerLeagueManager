using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.DeletePlayer
{
    [TestClass]
    public class DeletePlayerTwiceTest : BaseCommandTest
    {
        private Guid _playerId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new PlayerCreatedEvent() { PlayerId = _playerId, PlayerName = "Homer Simpson" };
            yield return new PlayerDeletedEvent() { PlayerId = _playerId };
        }

        [TestMethod]
        public void DeletePlayerTwice()
        {
            RunTest(new DeletePlayerCommand() { PlayerId = _playerId });
        }

        public override Exception ExpectedException()
        {
            return new PlayerAlreadyDeletedException(_playerId);
        }
    }
}
