using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Commands.Domain.Aggregates;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Commands.Domain.Infrastructure.Exceptions;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class CommandHandlerFactoryTests
    {
        [TestMethod]
        public void ShouldLogCommand()
        {
            var testCommand = new EnterGameResultsCommand();
            testCommand.GameDate = DateTime.Now.AddDays(-2);
            testCommand.CommandId = Guid.NewGuid();
            testCommand.GameId = Guid.NewGuid();
            testCommand.IPAddress = "12.34.56.78";
            testCommand.User = "dylans";
            testCommand.Timestamp = DateTime.Now;
            
            var newPlayerA = new EnterGameResultsCommand.GamePlayer();
            newPlayerA.PlayerName = "Dylan Smith";
            newPlayerA.Placing = 1;
            newPlayerA.Winnings = 120;
            
            var newPlayerB = new EnterGameResultsCommand.GamePlayer();
            newPlayerB.PlayerName = "Homer Simpson";
            newPlayerB.Placing = 2;
            newPlayerB.Winnings = 0;

            var players = new List<EnterGameResultsCommand.GamePlayer>();
            players.Add(newPlayerA);
            players.Add(newPlayerB);

            testCommand.Players = players;

            var mockCommandRepository = new Mock<ICommandRepository>();

            var sut = new CommandHandlerFactory(
                new Mock<IEventRepository>().Object,
                new Mock<IQueryService>().Object,
                mockCommandRepository.Object);

            sut.ExecuteCommand(testCommand);

            mockCommandRepository.Verify(x => x.LogCommand(testCommand));
        }
    }
}
