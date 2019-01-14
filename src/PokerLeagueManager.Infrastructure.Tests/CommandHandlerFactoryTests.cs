using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class CommandHandlerFactoryTests
    {
        [TestMethod]
        public void ShouldLogCommand()
        {
            var testCommand = new CreateGameCommand();
            testCommand.GameDate = DateTime.Now.AddDays(-2);
            testCommand.CommandId = Guid.NewGuid();
            testCommand.GameId = Guid.NewGuid();
            testCommand.IPAddress = "12.34.56.78";
            testCommand.Timestamp = DateTime.Now;

            var mockCommandRepository = new Mock<ICommandRepository>();
            var mockQueryService = new Mock<IQueryService>();

            mockQueryService.Setup(q => q.Execute(It.IsAny<GetGameCountByDateQuery>())).Returns(0);

            var sut = new CommandHandlerFactory(
                new Mock<IEventRepository>().Object,
                mockQueryService.Object,
                mockCommandRepository.Object);

            sut.ExecuteCommand(testCommand);

            mockCommandRepository.Verify(x => x.LogCommand(testCommand));
        }

        [TestMethod]
        public void ShouldLogFailedCommand()
        {
            var testCommand = new CreateGameCommand();
            testCommand.GameDate = DateTime.Now.AddDays(-2);
            testCommand.CommandId = Guid.NewGuid();
            testCommand.GameId = Guid.NewGuid();
            testCommand.IPAddress = "12.34.56.78";
            testCommand.Timestamp = DateTime.Now;

            var mockCommandRepository = new Mock<ICommandRepository>();
            var mockEventRepository = new Mock<IEventRepository>();
            var mockQueryService = new Mock<IQueryService>();

            mockQueryService.Setup(q => q.Execute(It.IsAny<GetGameCountByDateQuery>())).Returns(0);

            var sut = new CommandHandlerFactory(
                mockEventRepository.Object,
                mockQueryService.Object,
                mockCommandRepository.Object);

            var ex = new ArgumentException("foo");
            mockEventRepository.Setup(x => x.PublishEvents(It.IsAny<IAggregateRoot>(), testCommand)).Throws(ex);

            try
            {
                sut.ExecuteCommand(testCommand);
            }
            catch
            {
                // eat all exceptions
            }

            mockCommandRepository.Verify(x => x.LogCommand(testCommand));
            mockCommandRepository.Verify(x => x.LogFailedCommand(testCommand, ex));
        }
    }
}
