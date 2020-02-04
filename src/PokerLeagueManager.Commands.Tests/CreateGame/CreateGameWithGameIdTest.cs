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
    public class CreateGameWithGameIdTest : BaseCommandTest
    {
        private readonly DateTime _gameDate = DateTime.Parse("03-Jul-1981");
        private readonly Guid _gameId = Guid.NewGuid();

        [TestMethod]
        public void CreateGameWithGameId()
        {
            RunTest(new CreateGameCommand() { GameId = _gameId, GameDate = _gameDate });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new GameCreatedEvent() { GameId = _gameId, GameDate = _gameDate };
        }
    }
}
