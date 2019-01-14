﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.Commands.Tests.DeleteGame
{
    [TestClass]
    public class DeleteGameInvalidGameIdTest : BaseCommandTest
    {
        [TestMethod]
        public void DeleteGameInvalidGameId()
        {
            RunTest(new DeleteGameCommand() { GameId = Guid.NewGuid() });
        }

        public override Exception ExpectedException()
        {
            return new ArgumentException();
        }
    }
}
