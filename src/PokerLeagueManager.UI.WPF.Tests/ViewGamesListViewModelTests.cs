using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Tests.Infrastructure;
using PokerLeagueManager.UI.Wpf.ViewModels;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.Tests
{
    [TestClass]
    public class ViewGamesListViewModelTests
    {
        [TestMethod]
        public void WhenNoGames_ShowsEmptyList()
        {
            var mockQueryService = new Mock<IQueryService>();
            var mockMainWindow = new Mock<IMainWindow>();

            var emptyGamesList = new List<GetGamesListDto>();

            mockQueryService.Setup(x => x.GetGamesList()).Returns(emptyGamesList);

            var sut = new ViewGamesListViewModel(mockQueryService.Object, mockMainWindow.Object);

            Assert.AreEqual(0, sut.Games.Count());
        }

        [TestMethod]
        public void OneGames_ShowsProperFormat()
        {
            var mockQueryService = new Mock<IQueryService>();
            var mockMainWindow = new Mock<IMainWindow>();

            var oneGameList = new List<GetGamesListDto>();
            oneGameList.Add(new GetGamesListDto()
            {
                GameDate = DateTime.Parse("12-Feb-2014"),
                Winner = "Dylan",
                Winnings = 100
            });

            mockQueryService.Setup(x => x.GetGamesList()).Returns(oneGameList);

            var sut = new ViewGamesListViewModel(mockQueryService.Object, mockMainWindow.Object);

            Assert.AreEqual(1, sut.Games.Count());
            Assert.AreEqual("12-Feb-2014 - Dylan [$100]", sut.Games.First());
        }

        [TestMethod]
        public void ThreeGames_ShowsInOrder()
        {
            var mockQueryService = new Mock<IQueryService>();
            var mockMainWindow = new Mock<IMainWindow>();

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

            mockQueryService.Setup(x => x.GetGamesList()).Returns(threeGameList);

            var sut = new ViewGamesListViewModel(mockQueryService.Object, mockMainWindow.Object);

            Assert.AreEqual(3, sut.Games.Count());
            Assert.AreEqual("14-Feb-2014 - Dylan [$100]", sut.Games.ElementAt(0));
            Assert.AreEqual("13-Feb-2014 - Dylan [$100]", sut.Games.ElementAt(1));
            Assert.AreEqual("13-Feb-2013 - Dylan [$100]", sut.Games.ElementAt(2));
        }

        [TestMethod]
        public void AddGame_ShowsEnterGameResultsView()
        {
            var mockQueryService = new Mock<IQueryService>();
            var mockMainWindow = new Mock<IMainWindow>();
            var mockView = new Mock<IEnterGameResultsView>();
            Resolver.Container.RegisterInstance<IEnterGameResultsView>(mockView.Object);

            var sut = new ViewGamesListViewModel(mockQueryService.Object, mockMainWindow.Object);

            sut.AddGameCommand.Execute(null);

            mockMainWindow.Verify(x => x.ShowView(mockView.Object));
        }

        [TestMethod]
        public void AddGameCanExecute_ReturnsTrue()
        {
            var mockQueryService = new Mock<IQueryService>();
            var mockMainWindow = new Mock<IMainWindow>();

            var sut = new ViewGamesListViewModel(mockQueryService.Object, mockMainWindow.Object);

            Assert.IsTrue(sut.AddGameCommand.CanExecute(null));
        }
    }
}
