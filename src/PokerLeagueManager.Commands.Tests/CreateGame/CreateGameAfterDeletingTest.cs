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
    public class CreateGameAfterDeletingTest : BaseCommandTest
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");
        private Guid _gameId = Guid.NewGuid();

        public override IEnumerable<IEvent> Given()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = _gameDate };
            yield return new GameDeletedEvent() { GameId = _gameId };
        }

        [TestMethod]
        public void CreateGameAfterDeleting()
        {
            RunTest(new CreateGameCommand() { GameDate = _gameDate });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameCreatedEvent() { GameId = AnyGuid(), GameDate = _gameDate };
        }
    }
}
