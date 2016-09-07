using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.CodedUITests.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.Tests
{
    [CodedUITest]
    public class RenamePlayerDuplicatePlayersInGameTest
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
        public void RenamePlayerDuplicatePlayerInGame()
        {
            var testDate = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Peewee Herman", placing: "1", winnings: "130", payIn: "75")
                            .AddPlayer("Mister Rogers", placing: "2", winnings: "20", payIn: "75")
                            .ClickSaveGame();

            _gamesListScreen.ClickPlayerStatistics()
                            .DoubleClickPlayer("Mister Rogers")
                            .EnterNewPlayerName("peewee herman")
                            .ClickRenamePlayer()
                            .ClickConfirmMerge()
                            .VerifyDuplicatePlayerWarning()
                            .DismissWarningDialog()
                            .VerifyPlayerName("Mister Rogers");
        }
    }
}
