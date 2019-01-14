﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.CreatePlayer
{
    [TestClass]
    public class CreatePlayerWithEmptyNameTest : BaseCommandTest
    {
        private string _playerName = "   ";

        [TestMethod]
        public void CreatePlayerWithEmptyName()
        {
            RunTest(new CreatePlayerCommand() { PlayerName = _playerName });
        }

        public override Exception ExpectedException()
        {
            return new InvalidPlayerNameException(_playerName);
        }
    }
}
