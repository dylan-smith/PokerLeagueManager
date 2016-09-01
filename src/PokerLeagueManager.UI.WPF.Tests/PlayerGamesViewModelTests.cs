using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using log4net;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Commands.Domain.Exceptions;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.ViewModels;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.Tests
{
    [TestClass]
    public class PlayerGamesViewModelTests
    {
        private Mock<ICommandService> _mockCommandService = new Mock<ICommandService>();
        private Mock<IQueryService> _mockQueryService = new Mock<IQueryService>();
        private Mock<IMainWindow> _mockMainWindow = new Mock<IMainWindow>();
        private Mock<ILog> _mockLogger = new Mock<ILog>();

        private PlayerGamesViewModel _sut = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCommandService = new Mock<ICommandService>();
            _mockQueryService = new Mock<IQueryService>();
            _mockMainWindow = new Mock<IMainWindow>();
        }

        [TestMethod]
        public void WhenClickClose_ShowPlayerStatisticsView()
        {
            var mockView = new Mock<IPlayerStatisticsView>();
            Resolver.Container.RegisterInstance<IPlayerStatisticsView>(mockView.Object);

            _sut = CreateSUT();

            _sut.CloseCommand.Execute(null);

            _mockMainWindow.Verify(x => x.ShowView(mockView.Object));
        }

        [TestMethod]
        public void WhenRenameMerge_ShowConfirmation()
        {
            var playerDto = new GetPlayerByNameDto();
            _mockQueryService.Setup(q => q.GetPlayerByName("Ronald McDonald")).Returns(playerDto);
            _mockMainWindow.Setup(w => w.ShowConfirmation(It.IsAny<string>(), It.IsAny<string>())).Returns(MessageBoxResult.OK);

            _sut = CreateSUT();
            _sut.PlayerName = "Burger King";
            _sut.NewPlayerName = "Ronald McDonald";

            _sut.RenamePlayerCommand.Execute(null);

            _mockMainWindow.Verify(w => w.ShowConfirmation(It.IsAny<string>(), It.IsAny<string>()));
            _mockCommandService.Verify(c => c.ExecuteCommand(It.IsAny<RenamePlayerCommand>()));
            Assert.AreEqual("Ronald McDonald", _sut.PlayerName);
        }

        [TestMethod]
        public void WhenRenameMergeWithoutConfirmation_DoNotExecuteCommand()
        {
            var playerDto = new GetPlayerByNameDto();
            _mockQueryService.Setup(q => q.GetPlayerByName("Ronald McDonald")).Returns(playerDto);
            _mockMainWindow.Setup(w => w.ShowConfirmation(It.IsAny<string>(), It.IsAny<string>())).Returns(MessageBoxResult.Cancel);

            _sut = CreateSUT();
            _sut.PlayerName = "Burger King";
            _sut.NewPlayerName = "Ronald McDonald";

            _sut.RenamePlayerCommand.Execute(null);

            _mockMainWindow.Verify(w => w.ShowConfirmation(It.IsAny<string>(), It.IsAny<string>()));
            _mockCommandService.Verify(c => c.ExecuteCommand(It.IsAny<RenamePlayerCommand>()), Times.Never());
            Assert.AreEqual("Burger King", _sut.PlayerName);
        }

        [TestMethod]
        public void WhenRenameToDuplicate_ShowWarning()
        {
            var customException = new DuplicatePlayerNameException("Macho Man");
            var ex = new FaultException<ExceptionDetail>(new ExceptionDetail(customException));
            _mockCommandService.Setup(x => x.ExecuteCommand(It.IsAny<RenamePlayerCommand>())).Throws(ex);

            _sut = CreateSUT();
            _sut.PlayerName = "Hulk Hogan";
            _sut.NewPlayerName = "Macho Man";

            _sut.RenamePlayerCommand.Execute(null);

            _mockMainWindow.Verify(x => x.ShowWarning("Action Failed", customException.Message));
            Assert.AreEqual("Hulk Hogan", _sut.PlayerName);
        }

        [TestMethod]
        public void WhenNoGames_ShowsEmptyList()
        {
            var emptyGamesList = new List<GetPlayerGamesDto>();

            _mockQueryService.Setup(x => x.GetPlayerGames("Darcy Lussier")).Returns(emptyGamesList);

            _sut = CreateSUT();
            _sut.PlayerName = "Darcy Lussier";

            Assert.AreEqual(0, _sut.Games.Count());
        }

        [TestMethod]
        public void OneGames_ShowsProperFormat()
        {
            var oneGameList = new List<GetPlayerGamesDto>();
            oneGameList.Add(new GetPlayerGamesDto()
            {
                GameDate = DateTime.Parse("12-Feb-2014"),
                PlayerName = "Darcy Lussier",
                Placing = 3,
                Winnings = 10,
                PayIn = 20
            });

            _mockQueryService.Setup(x => x.GetPlayerGames("Darcy Lussier")).Returns(oneGameList);

            _sut = CreateSUT();
            _sut.PlayerName = "Darcy Lussier";

            Assert.AreEqual(1, _sut.Games.Count());
            Assert.AreEqual("12-Feb-2014 - Placing: 3 - Winnings: $10 - Pay In: $20", _sut.Games.First());
        }

        [TestMethod]
        public void ThreeGames_ShowsInOrder()
        {
            var threeGameList = new List<GetPlayerGamesDto>();

            threeGameList.Add(new GetPlayerGamesDto()
            {
                GameDate = DateTime.Parse("12-Feb-2014"),
                PlayerName = "Darcy Lussier",
                Placing = 3,
                Winnings = 10,
                PayIn = 20
            });

            threeGameList.Add(new GetPlayerGamesDto()
            {
                GameDate = DateTime.Parse("1-Jan-2015"),
                PlayerName = "Darcy Lussier",
                Placing = 1,
                Winnings = 110,
                PayIn = 20
            });

            threeGameList.Add(new GetPlayerGamesDto()
            {
                GameDate = DateTime.Parse("17-Oct-2014"),
                PlayerName = "Darcy Lussier",
                Placing = 7,
                Winnings = 0,
                PayIn = 40
            });

            _mockQueryService.Setup(x => x.GetPlayerGames("Darcy Lussier")).Returns(threeGameList);

            _sut = CreateSUT();
            _sut.PlayerName = "Darcy Lussier";

            Assert.AreEqual(3, _sut.Games.Count());
            Assert.AreEqual("01-Jan-2015 - Placing: 1 - Winnings: $110 - Pay In: $20", _sut.Games.ElementAt(0));
            Assert.AreEqual("17-Oct-2014 - Placing: 7 - Winnings: $0 - Pay In: $40", _sut.Games.ElementAt(1));
            Assert.AreEqual("12-Feb-2014 - Placing: 3 - Winnings: $10 - Pay In: $20", _sut.Games.ElementAt(2));
        }

        private PlayerGamesViewModel CreateSUT()
        {
            var sut = new PlayerGamesViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, _mockLogger.Object);
            return sut;
        }
    }
}
