using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.ViewModels;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.Tests
{
    [TestClass]
    public class ViewGamesListViewModelTests
    {
        private Mock<ICommandService> _mockCommandService = new Mock<ICommandService>();
        private Mock<IQueryService> _mockQueryService = new Mock<IQueryService>();
        private Mock<IMainWindow> _mockMainWindow = new Mock<IMainWindow>();

        private ViewGamesListViewModel _sut = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCommandService = new Mock<ICommandService>();
            _mockQueryService = new Mock<IQueryService>();
            _mockMainWindow = new Mock<IMainWindow>();
        }

        [TestMethod]
        public void WhenNoGames_ShowsEmptyList()
        {
            var emptyGamesList = new List<GetGamesListDto>();

            _mockQueryService.Setup(x => x.GetGamesList()).Returns(emptyGamesList);

            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            Assert.AreEqual(0, _sut.Games.Count());
        }

        [TestMethod]
        public void OneGames_ShowsProperFormat()
        {
            var oneGameList = new List<GetGamesListDto>();
            oneGameList.Add(new GetGamesListDto()
            {
                GameDate = DateTime.Parse("12-Feb-2014"),
                Winner = "Dylan",
                Winnings = 100
            });

            _mockQueryService.Setup(x => x.GetGamesList()).Returns(oneGameList);

            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            Assert.AreEqual(1, _sut.Games.Count());
            Assert.AreEqual("12-Feb-2014 - Dylan [$100]", _sut.Games.First());
        }

        [TestMethod]
        public void ThreeGames_ShowsInOrder()
        {
            var threeGameList = new List<GetGamesListDto>();

            threeGameList.Add(new GetGamesListDto()
            {
                GameDate = DateTime.Parse("13-Feb-2014"),
                Winner = "Dylan",
                Winnings = 100
            });

            threeGameList.Add(new GetGamesListDto()
            {
                GameDate = DateTime.Parse("13-Feb-2013"),
                Winner = "Dylan",
                Winnings = 100
            });

            threeGameList.Add(new GetGamesListDto()
            {
                GameDate = DateTime.Parse("14-Feb-2014"),
                Winner = "Dylan",
                Winnings = 100
            });

            _mockQueryService.Setup(x => x.GetGamesList()).Returns(threeGameList);

            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            Assert.AreEqual(3, _sut.Games.Count());
            Assert.AreEqual("14-Feb-2014 - Dylan [$100]", _sut.Games.ElementAt(0));
            Assert.AreEqual("13-Feb-2014 - Dylan [$100]", _sut.Games.ElementAt(1));
            Assert.AreEqual("13-Feb-2013 - Dylan [$100]", _sut.Games.ElementAt(2));
        }

        [TestMethod]
        public void AddGame_ShowsEnterGameResultsView()
        {
            var mockView = new Mock<IEnterGameResultsView>();
            Resolver.Container.RegisterInstance<IEnterGameResultsView>(mockView.Object);

            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            _sut.AddGameCommand.Execute(null);

            _mockMainWindow.Verify(x => x.ShowView(mockView.Object));
        }

        [TestMethod]
        public void AddGameCanExecute_ReturnsTrue()
        {
            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            Assert.IsTrue(_sut.AddGameCommand.CanExecute(null));
        }

        [TestMethod]
        public void DoubleClickGame_ShowsViewGameResultsView()
        {
            var mockView = new Mock<IViewGameResultsView>();
            Resolver.Container.RegisterInstance<IViewGameResultsView>(mockView.Object);

            var gameId = Guid.NewGuid();
            var gamesList = new List<GetGamesListDto>();
            gamesList.Add(new GetGamesListDto() { GameId = gameId });

            _mockQueryService.Setup(q => q.GetGamesList()).Returns(gamesList);

            _sut = new ViewGamesListViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);

            _sut.SelectedGameIndex = 0;
            _sut.GameDoubleClickCommand.Execute(null);

            _mockMainWindow.Verify(x => x.ShowView(mockView.Object));
            mockView.VerifySet(x => x.GameId = gameId);
        }
    }
}
