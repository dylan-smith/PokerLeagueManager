using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests
{
    [CodedUITest]
    public class RenamePlayerHappyPathTest
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
        public void RenamePlayerHappyPath()
        {
            var testDate = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .EnterPlayerName("Peewee Herman")
                            .EnterPlacing("1")
                            .EnterWinnings("130")
                            .EnterPayIn("75")
                            .ClickAddPlayer()
                            .EnterPlayerName("Mister Rogers")
                            .EnterPlacing("2")
                            .EnterWinnings("20")
                            .EnterPayIn("75")
                            .ClickAddPlayer()
                            .ClickSaveGame()
                            .ClickPlayerStatistics()
                            .DoubleClickPlayer("Mister Rogers")
                            .EnterNewPlayerName("Mr Dressup")
                            .ClickRenamePlayer()
                            .VerifyPlayerName("Mr Dressup")
                            .ClickClose()
                            .VerifyPlayerInList("Mr Dressup");
        }
    }
}
