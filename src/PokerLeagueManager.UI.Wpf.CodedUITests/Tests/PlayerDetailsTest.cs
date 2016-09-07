using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.CodedUITests.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.Tests
{
    [CodedUITest]
    public class PlayerDetailsTest
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
        public void PlayerDetails()
        {
            _gamesListScreen.DeleteAllGames();

            var testDate1 = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate1)
                            .AddPlayer("Leonardo", placing: "1", winnings: "130", payIn: "75")
                            .AddPlayer("Donatello", placing: "2", winnings: "20", payIn: "75")
                            .ClickSaveGame();

            var testDate2 = _gamesListScreen.FindUnusedGameDate();

            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate2)
                            .AddPlayer("Raphael", placing: "1", winnings: "125", payIn: "75")
                            .AddPlayer("Donatello", placing: "2", winnings: "0", payIn: "50")
                            .ClickSaveGame();

            _gamesListScreen.ClickPlayerStatistics()
                            .DoubleClickPlayer("Donatello")
                            .VerifyPlayerName("Donatello")
                            .VerifyGameCount(2)
                            .VerifyGameInList(DateTime.Parse(testDate1), placing: 2, winnings: 20, payIn: 75)
                            .VerifyGameInList(DateTime.Parse(testDate2), placing: 2, winnings: 0, payIn: 50)
                            .ClickClose()
                            .DoubleClickPlayer("Leonardo")
                            .VerifyGameCount(1)
                            .VerifyGameInList(DateTime.Parse(testDate1), placing: 1, winnings: 130, payIn: 75)
                            .ClickClose()
                            .DoubleClickPlayer("Raphael")
                            .VerifyGameCount(1)
                            .VerifyGameInList(DateTime.Parse(testDate2), placing: 1, winnings: 125, payIn: 75);
        }
    }
}
