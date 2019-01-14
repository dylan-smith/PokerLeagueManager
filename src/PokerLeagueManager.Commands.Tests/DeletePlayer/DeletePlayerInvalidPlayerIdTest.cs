using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Tests.DeletePlayer
{
    [TestClass]
    public class DeletePlayerInvalidPlayerIdTest : BaseCommandTest
    {
        [TestMethod]
        public void DeletePlayerInvalidPlayerId()
        {
            RunTest(new DeletePlayerCommand() { PlayerId = Guid.NewGuid() });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
