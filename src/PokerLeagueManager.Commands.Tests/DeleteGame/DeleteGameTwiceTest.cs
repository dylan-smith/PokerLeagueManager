using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.DeleteGame
{
    [TestClass]
    public class DeleteGameTwiceTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
            yield return new GameDeletedEvent() { GameId = _gameId };
        }

        [TestMethod]
        public void DeleteGameTwice()
        {
            RunTest(new DeleteGameCommand() { GameId = _gameId });
        }

        public override Exception ExpectedException()
        {
            return new GameAlreadyDeletedException(_gameId);
        }
    }
}
