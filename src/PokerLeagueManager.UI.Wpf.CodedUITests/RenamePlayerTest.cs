using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests
{
    [CodedUITest]
    public class RenamePlayerTest
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
        public void RenamePlayer()
        {
            var testDate = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Peewee Herman", placing: "1", winnings: "130", payIn: "75")
                            .AddPlayer("Mister Rogers", placing: "2", winnings: "20", payIn: "75")
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
