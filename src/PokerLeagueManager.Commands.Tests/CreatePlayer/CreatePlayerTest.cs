using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.CreatePlayer
{
    [TestClass]
    public class CreatePlayerTest : BaseCommandTest
    {
        private string _playerName = "Homer Simpson";

        [TestMethod]
        public void CreatePlayer()
        {
            RunTest(new CreatePlayerCommand() { PlayerName = _playerName });
        }

        public override IEnumerable<IEvent> ExpectedEvents()
        {
            yield return new PlayerCreatedEvent() { PlayerId = AnyGuid(), PlayerName = _playerName };
        }
    }
}
