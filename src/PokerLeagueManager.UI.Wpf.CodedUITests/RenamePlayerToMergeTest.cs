using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests
{
    [CodedUITest]
    public class RenamePlayerToMergeTest
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
        public void RenamePlayerToMerge()
        {
            _gamesListScreen.DeleteAllGames();

            var testDate = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Peewee Herman", placing: "1", winnings: "130", payIn: "75")
                            .AddPlayer("Mister Rogers", placing: "2", winnings: "20", payIn: "75")
                            .ClickSaveGame();

            testDate = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Peewee Herman", placing: "1", winnings: "130", payIn: "75")
                            .AddPlayer("Hulk Hogan", placing: "2", winnings: "20", payIn: "75")
                            .ClickSaveGame();

            _gamesListScreen.ClickPlayerStatistics()
                            .DoubleClickPlayer("Mister Rogers")
                            .EnterNewPlayerName("hulk hogan")
                            .ClickRenamePlayer()
                            .VerifyPlayerName("hulk hogan")
                            .ClickClose()
                            .VerifyPlayerInList("Hulk Hogan", gamesPlayed: 2);
        }
    }
}
