﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Tests.Infrastructure;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities.Exceptions;

namespace PokerLeagueManager.Commands.Tests.EnterGameResults
{
    [TestClass]
    public class OnePlayerTest : BaseTestFixture
    {
        private DateTime _gameDate = DateTime.Parse("03-Jul-1981");

        [TestMethod]
        public void OnePlayer()
        {
            var players = new List<EnterGameResultsCommand.GamePlayer>();
            players.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = "Dylan Smith", Placing = 1, Winnings = 100 });

            RunTest(new EnterGameResultsCommand() { GameDate = _gameDate, Players = players });
        }

        public override Exception ExpectedException()
        {
            return new GameWithNotEnoughPlayersException();
        }
    }
}