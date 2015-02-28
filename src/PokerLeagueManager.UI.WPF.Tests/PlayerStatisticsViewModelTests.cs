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
    public class PlayerStatisticsViewModelTests
    {
        private Mock<ICommandService> _mockCommandService = new Mock<ICommandService>();
        private Mock<IQueryService> _mockQueryService = new Mock<IQueryService>();
        private Mock<IMainWindow> _mockMainWindow = new Mock<IMainWindow>();

        private PlayerStatisticsViewModel _sut = null;

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
            var emptyPlayersList = new List<GetPlayerStatisticsDto>();

            _mockQueryService.Setup(x => x.GetPlayerStatistics()).Returns(emptyPlayersList);

            _sut = CreateSUT();

            Assert.AreEqual(0, _sut.Players.Count());
        }

        [TestMethod]
        public void OnePlayer_ShowsProperFormat()
        {
            var onePlayerList = new List<GetPlayerStatisticsDto>();
            onePlayerList.Add(new GetPlayerStatisticsDto()
            {
                PlayerName = "Homer Simpson",
                GamesPlayed = 3,
                Winnings = 120,
                PayIn = 30,
                Profit = 90,
                ProfitPerGame = 30
            });

            _mockQueryService.Setup(x => x.GetPlayerStatistics()).Returns(onePlayerList);

            _sut = CreateSUT();

            Assert.AreEqual(1, _sut.Players.Count());
            Assert.AreEqual("Homer Simpson - Games Played: 3 - Winnings: $120 - Pay In: $30 - Profit: $90 - Profit Per Game: $30", _sut.Players.First());
        }

        [TestMethod]
        public void ThreePlayers_ShowsInOrder()
        {
            var threePlayerList = new List<GetPlayerStatisticsDto>();

            threePlayerList.Add(new GetPlayerStatisticsDto()
            {
                PlayerName = "Brad Pitt",
                GamesPlayed = 3,
                Winnings = 120,
                PayIn = 30,
                Profit = 90,
                ProfitPerGame = 30
            });

            threePlayerList.Add(new GetPlayerStatisticsDto()
            {
                PlayerName = "Angelina Jolie",
                GamesPlayed = 1,
                Winnings = 0,
                PayIn = 20,
                Profit = -20,
                ProfitPerGame = -20
            });

            threePlayerList.Add(new GetPlayerStatisticsDto()
            {
                PlayerName = "Keira Knightly",
                GamesPlayed = 4,
                Winnings = 710,
                PayIn = 70,
                Profit = 640,
                ProfitPerGame = 128
            });

            _mockQueryService.Setup(x => x.GetPlayerStatistics()).Returns(threePlayerList);

            _sut = CreateSUT();

            Assert.AreEqual(3, _sut.Players.Count());
            Assert.IsTrue(_sut.Players.ElementAt(0).Contains("Keira"));
            Assert.IsTrue(_sut.Players.ElementAt(1).Contains("Brad"));
            Assert.IsTrue(_sut.Players.ElementAt(2).Contains("Angelina"));
        }

        [TestMethod]
        public void WhenClickGames_ShowGamesListView()
        {
            var mockView = new Mock<IViewGamesListView>();
            Resolver.Container.RegisterInstance<IViewGamesListView>(mockView.Object);

            _sut = CreateSUT();

            _sut.GamesCommand.Execute(null);

            _mockMainWindow.Verify(x => x.ShowView(mockView.Object));
        }

        private PlayerStatisticsViewModel CreateSUT()
        {
            var sut = new PlayerStatisticsViewModel(_mockCommandService.Object, _mockQueryService.Object, _mockMainWindow.Object, null);
            return sut;
        }
    }
}
