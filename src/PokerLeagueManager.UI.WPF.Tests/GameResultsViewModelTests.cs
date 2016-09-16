using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Tests.Infrastructure;
using PokerLeagueManager.UI.Wpf.ViewModels;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.Tests
{
    [TestClass]
    public class GameResultsViewModelTests
    {
        [TestMethod]
        public void WhenGameDateIsSet_GameDateTextIsSet()
        {
            var sut = new GameResultsViewModel(null, null, null, null);
            var watcher = new NotifyPropertyChangedWatcher(sut);

            sut.GameDate = DateTime.Parse("01-Jan-2015");

            Assert.AreEqual("1-Jan-2015", sut.GameDateText);
            Assert.IsTrue(watcher.HasPropertyChanged("GameDateText"));
        }

        [TestMethod]
        public void WhenGameIdIsSet_PlayersIsUpdated()
        {
            var gameId = Guid.NewGuid();

            var player = new GetGamePlayersDto();
            player.GameId = gameId;
            player.Placing = 1;
            player.PlayerName = "King Kong";
            player.Winnings = 100;
            player.PayIn = 20;

            var testResultsDto = new List<GetGamePlayersDto>() { player };

            var mockQuerySvc = new Mock<IQueryService>();
            mockQuerySvc.Setup(x => x.Execute(It.Is<GetGamePlayersQuery>(q => q.GameId == gameId)))
                        .Returns(testResultsDto);

            var sut = new GameResultsViewModel(null, mockQuerySvc.Object, null, null);

            sut.GameId = gameId;

            Assert.AreEqual(1, sut.Players.Count());
            Assert.AreEqual("1 - King Kong [Win: $100] [Pay: $20]", sut.Players.First());
        }

        [TestMethod]
        public void WhenGameIdIsSet_NotifyPropertyChangedShouldFire()
        {
            var gameId = Guid.NewGuid();

            var player = new GetGamePlayersDto();
            player.GameId = gameId;
            player.Placing = 1;
            player.PlayerName = "King Kong";
            player.Winnings = 100;
            player.PayIn = 20;

            var testResultsDto = new List<GetGamePlayersDto>() { player };

            var mockQuerySvc = new Mock<IQueryService>();
            mockQuerySvc.Setup(x => x.Execute(It.Is<GetGamePlayersQuery>(q => q.GameId == gameId)))
                        .Returns(testResultsDto);

            var sut = new GameResultsViewModel(null, mockQuerySvc.Object, null, null);

            var watcher = new NotifyPropertyChangedWatcher(sut);

            sut.GameId = gameId;

            Assert.IsTrue(watcher.HasPropertyChanged("Players"));
        }

        [TestMethod]
        public void PlayersAreShownInOrderByPlacing()
        {
            var gameId = Guid.NewGuid();
            var testResultsDto = new List<GetGamePlayersDto>();

            var player1 = new GetGamePlayersDto() { Placing = 3, GameId = gameId, PlayerName = "King Kong", Winnings = 0, PayIn = 40 };
            var player2 = new GetGamePlayersDto() { Placing = 1, GameId = gameId, PlayerName = "Donkey Kong", Winnings = 100, PayIn = 40 };
            var player3 = new GetGamePlayersDto() { Placing = 2, GameId = gameId, PlayerName = "Diddy Kong", Winnings = 0, PayIn = 20 };

            testResultsDto.Add(player1);
            testResultsDto.Add(player2);
            testResultsDto.Add(player3);

            var mockQuerySvc = new Mock<IQueryService>();
            mockQuerySvc.Setup(x => x.Execute(It.Is<GetGamePlayersQuery>(q => q.GameId == gameId)))
                        .Returns(testResultsDto);

            var sut = new GameResultsViewModel(null, mockQuerySvc.Object, null, null);

            sut.GameId = gameId;

            Assert.AreEqual(3, sut.Players.Count());
            Assert.AreEqual("1 - Donkey Kong [Win: $100] [Pay: $40]", sut.Players.ElementAt(0));
            Assert.AreEqual("2 - Diddy Kong [Win: $0] [Pay: $20]", sut.Players.ElementAt(1));
            Assert.AreEqual("3 - King Kong [Win: $0] [Pay: $40]", sut.Players.ElementAt(2));
        }

        [TestMethod]
        public void ClickCloseButtonShouldShowViewGamesList()
        {
            var mockMainWindow = new Mock<IMainWindow>();

            var mockView = new Mock<IGamesListView>();
            Resolver.Container.RegisterInstance<IGamesListView>(mockView.Object);

            var sut = new GameResultsViewModel(null, null, mockMainWindow.Object, null);

            sut.CloseCommand.Execute(null);

            mockMainWindow.Verify(x => x.ShowView(mockView.Object));
        }
    }
}
