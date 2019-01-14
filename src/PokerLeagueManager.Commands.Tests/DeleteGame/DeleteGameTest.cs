using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.DeleteGame
{
    [TestClass]
    public class DeleteGameTest : BaseCommandTest
    {
        private Guid _gameId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = DateTime.Now };
        }

        [TestMethod]
        public void DeleteGame()
        {
            RunTest(new DeleteGameCommand() { GameId = _gameId });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameDeletedEvent() { GameId = _gameId };
        }
    }
}
