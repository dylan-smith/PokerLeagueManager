﻿using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.CodedUITests.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.Tests
{
    [CodedUITest]
    public class NoGameDateTest
    {
        private ApplicationUnderTest _app;
        private GamesListScreen _gamesListScreen;

        [TestInitialize]
        public void TestInitialize()
        {
            _app = ApplicationUnderTest.Launch(@"C:\PokerLeagueManager.UI.Wpf\PokerLeagueManager.UI.Wpf.exe");
            _gamesListScreen = new GamesListScreen(_app);
        }

        [TestMethod]
        public void NoGameDate()
        {
            var enterGameScreen = _gamesListScreen.ClickAddGame();

            enterGameScreen.EnterPlayerName("Jerry Seinfeld")
                           .EnterPlacing("1")
                           .EnterWinnings("130")
                           .ClickAddPlayer()
                           .EnterPlayerName("Wayne Gretzky")
                           .EnterPlacing("2")
                           .EnterWinnings("20")
                           .ClickAddPlayer();

            enterGameScreen.VerifySaveGameIsDisabled();
        }
    }
}
