using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.UI.WPF.Tests.Infrastructure;
using PokerLeagueManager.UI.WPF.ViewModels;
using System;

namespace PokerLeagueManager.UI.WPF.Tests
{
    [TestClass]
    public class EnterGameResultsViewModelTests
    {
        [TestMethod]
        public void WhenGameDateIsEmpty_SaveCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.GameDate = null;

            Assert.IsFalse(sut.SaveGameCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenGameDateIsValid_SaveCommandCanExecuteIsTrue()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.GameDate = DateTime.Now;

            Assert.IsTrue(sut.SaveGameCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerDataIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = string.Empty;
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = string.Empty;

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerNameIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = string.Empty;
            sut.NewPlacing = "1";
            sut.NewWinnings = "100";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlacingIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Homer Simpson";
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = "100";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenWinningsIsEmpty_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Anderson Silva";
            sut.NewPlacing = "5";
            sut.NewWinnings = string.Empty;

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlayerDataIsValid_AddPlayerCommandCanExecuteIsTrue()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Keira Knightly";
            sut.NewPlacing = "1";
            sut.NewWinnings = "225";

            Assert.IsTrue(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenPlacingIsNotANumber_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Tom Brady";
            sut.NewPlacing = "1st";
            sut.NewWinnings = "150";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void WhenWinningsIsNotANumber_AddPlayerCommandCanExecuteIsFalse()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Tom Brady";
            sut.NewPlacing = "1";
            sut.NewWinnings = "One Hundred Dollars";

            Assert.IsFalse(sut.AddPlayerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPlayerWithValidData()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

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
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.NewPlayerName = "Dylan Smith";
            sut.NewPlacing = string.Empty;
            sut.NewWinnings = "500";

            sut.AddPlayerCommand.Execute(null);
        }

        [TestMethod]
        public void SaveGameWithValidData()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            var watcher = new NotifyPropertyChangedWatcher(sut);

            sut.GameDate = DateTime.Now;
            sut.NewPlayerName = "foo";
            sut.NewPlacing = "foo";
            sut.NewWinnings = "foo";

            sut.SaveGameCommand.Execute(null);

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
        [ExpectedException(typeof(Exception))]
        public void SaveGameWithNoGameDate()
        {
            var sut = new EnterGameResultsViewModel(new Mock<ICommandService>().Object);

            sut.GameDate = null;

            sut.SaveGameCommand.Execute(null);
        }
    }
}
