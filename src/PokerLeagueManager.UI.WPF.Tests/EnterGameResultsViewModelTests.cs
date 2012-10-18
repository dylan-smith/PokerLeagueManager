using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.UI.WPF.Tests.Infrastructure;
using PokerLeagueManager.UI.WPF.ViewModels;
using System;
using System.Linq;

namespace PokerLeagueManager.UI.WPF.Tests
{
    [TestClass]
    public class EnterGameResultsViewModelTests
    {
        [TestMethod]
        public void WhenGameDateIsEmpty_SaveCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.GameDate = null;

            Assert.IsFalse(sut.SaveGameCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenGameDateIsValid_SaveCommandCanExecuteIsTrue()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.GameDate = DateTime.Now;

            Assert.IsTrue(sut.SaveGameCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerDataIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = string.Empty;
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = string.Empty;

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerNameIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = string.Empty;
            sut.NewPlacing = "1";
            sut.NewWinnings = "100";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlacingIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Homer Simpson";
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = "100";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenWinningsIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Anderson Silva";
            sut.NewPlacing = "5";
            sut.NewWinnings = string.Empty;

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerDataIsValid_AddPlayerCommandCanExecuteIsTrue()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Keira Knightly";
            sut.NewPlacing = "1";
            sut.NewWinnings = "225";

            Assert.IsTrue(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlacingIsNotANumber_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Tom Brady";
            sut.NewPlacing = "1st";
            sut.NewWinnings = "150";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenWinningsIsNotANumber_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Tom Brady";
            sut.NewPlacing = "1";
            sut.NewWinnings = "One Hundred Dollars";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPlayerWithValidData()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Dylan Smith";
            sut.NewPlacing = "1";
            sut.NewWinnings = "500";

            var watcher = new NotifyPropertyChangedWatcher(sut);

            bool playersChanged = false;
            sut.Players.CollectionChanged += delegate { playersChanged = true; };

            sut.AddPlayerCommand.Execute(null);

            Assert.AreEqual(string.Empty, sut.NewPlayerName);
            Assert.AreEqual(string.Empty, sut.NewPlacing);
            Assert.AreEqual(string.Empty, sut.NewWinnings);

            Assert.AreEqual(1, sut.Players.Count);
            Assert.AreEqual("Dylan Smith", sut.Players[0].PlayerName);
            Assert.AreEqual(1, sut.Players[0].Placing);
            Assert.AreEqual(500, sut.Players[0].Winnings);

            Assert.IsTrue(watcher.HasPropertyChanged("NewPlayerName"));
            Assert.IsTrue(watcher.HasPropertyChanged("NewPlacing"));
            Assert.IsTrue(watcher.HasPropertyChanged("NewWinnings"));
            Assert.IsTrue(playersChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IfAddPlayerCommandExecuteIsCalledWhenThereIsInvalidData_ShouldThrowException()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.NewPlayerName = "Dylan Smith";
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = "500";

            sut.AddPlayerCommand.Execute(null);
        }

        [TestMethod]
        public void SaveGameWithValidDataNoPlayers()
        {
            var fakeCommandService = new FakeCommandService();

            var sut = new EnterGameResultsViewModel(fakeCommandService);

            var watcher = new NotifyPropertyChangedWatcher(sut);

            var testGameDate = DateTime.Now;

            sut.GameDate = testGameDate;
            sut.NewPlayerName = "foo";
            sut.NewPlacing = "foo";
            sut.NewWinnings = "foo";

            sut.SaveGameCommand.Execute(null);

            Assert.AreEqual(1, fakeCommandService.ExecutedCommands.Count);
            EnterGameResultsCommand actualCommand = fakeCommandService.ExecutedCommands[0] as EnterGameResultsCommand;
            Assert.AreEqual(testGameDate, actualCommand.GameDate);
            Assert.AreEqual(0, actualCommand.Players.Count());

            Assert.IsNull(sut.GameDate);
            Assert.AreEqual(string.Empty, sut.NewPlayerName);
            Assert.AreEqual(string.Empty, sut.NewPlacing);
            Assert.AreEqual(string.Empty, sut.NewWinnings);

            Assert.AreEqual(0, sut.Players.Count);

            Assert.IsTrue(watcher.HasPropertyChanged("GameDate"));
            Assert.IsTrue(watcher.HasPropertyChanged("NewPlayerName"));
            Assert.IsTrue(watcher.HasPropertyChanged("NewPlacing"));
            Assert.IsTrue(watcher.HasPropertyChanged("NewWinnings"));
            Assert.IsTrue(watcher.HasPropertyChanged("Players"));
        }

        [TestMethod]
        public void SaveGameWithValidDataTwoPlayers()
        {
            var fakeCommandService = new FakeCommandService();

            var sut = new EnterGameResultsViewModel(fakeCommandService);

            var testGameDate = DateTime.Now;

            sut.GameDate = testGameDate;

            sut.NewPlayerName = "Ryan Fritsch";
            sut.NewPlacing = "1";
            sut.NewWinnings = "200";
            sut.AddPlayerCommand.Execute(null);

            sut.NewPlayerName = "Dylan Smith";
            sut.NewPlacing = "2";
            sut.NewWinnings = "0";
            sut.AddPlayerCommand.Execute(null);

            sut.SaveGameCommand.Execute(null);

            Assert.AreEqual(1, fakeCommandService.ExecutedCommands.Count);

            EnterGameResultsCommand actualCommand = fakeCommandService.ExecutedCommands[0] as EnterGameResultsCommand;

            Assert.AreEqual(testGameDate, actualCommand.GameDate);
            Assert.AreEqual(2, actualCommand.Players.Count());

            var ryanPlayer = actualCommand.Players.First(x => x.PlayerName == "Ryan Fritsch");
            var dylanPlayer = actualCommand.Players.First(x => x.PlayerName == "Dylan Smith");

            Assert.AreEqual(1, ryanPlayer.Placing);
            Assert.AreEqual(200, ryanPlayer.Winnings);

            Assert.AreEqual(2, dylanPlayer.Placing);
            Assert.AreEqual(0, dylanPlayer.Winnings);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SaveGameWithNoGameDate()
        {
            var sut = new EnterGameResultsViewModel(new FakeCommandService());

            sut.GameDate = null;

            sut.SaveGameCommand.Execute(null);
        }
    }
}
