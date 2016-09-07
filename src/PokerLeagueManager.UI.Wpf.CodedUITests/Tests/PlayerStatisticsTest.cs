using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.CodedUITests.TestFramework;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.Tests
{
    [CodedUITest]
    public class PlayerStatisticsTest
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
        public void PlayerStatistics()
        {
            _gamesListScreen.DeleteAllGames();

            var testDate = _gamesListScreen.FindUnusedGameDate();
            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Hulk Hogan", placing: "1", winnings: "100", payIn: "75")
                            .AddPlayer("Macho Man", placing: "2", winnings: "40", payIn: "25")
                            .AddPlayer("Undertaker", placing: "3", winnings: "0", payIn: "40")
                            .ClickSaveGame();

            testDate = _gamesListScreen.FindUnusedGameDate();
            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Ultimate Warrior", placing: "1", winnings: "90", payIn: "20")
                            .AddPlayer("Undertaker", placing: "2", winnings: "40", payIn: "80")
                            .AddPlayer("Hacksaw Jim Duggan", placing: "3", winnings: "0", payIn: "30")
                            .ClickSaveGame();

            testDate = _gamesListScreen.FindUnusedGameDate();
            _gamesListScreen.ClickAddGame()
                            .EnterGameDate(testDate)
                            .AddPlayer("Yokozuna", placing: "1", winnings: "310", payIn: "150")
                            .AddPlayer("Undertaker", placing: "2", winnings: "150", payIn: "100")
                            .AddPlayer("Hulk Hogan", placing: "3", winnings: "90", payIn: "200")
                            .AddPlayer("Macho Man", placing: "4", winnings: "0", payIn: "100")
                            .ClickSaveGame();

            _gamesListScreen.ClickPlayerStatistics()
                            .VerifyPlayerListCount(6)
                            .VerifyPlayerInList("Hulk Hogan", gamesPlayed: 2, winnings: 190, payIn: 275, profit: -85, profitPerGame: -42.5)
                            .VerifyPlayerInList("Macho Man", gamesPlayed: 2, winnings: 40, payIn: 125, profit: -85, profitPerGame: -42.5)
                            .VerifyPlayerInList("Undertaker", gamesPlayed: 3, winnings: 190, payIn: 220, profit: -30, profitPerGame: -10)
                            .VerifyPlayerInList("Ultimate Warrior", gamesPlayed: 1, winnings: 90, payIn: 20, profit: 70, profitPerGame: 70)
                            .VerifyPlayerInList("Hacksaw Jim Duggan", gamesPlayed: 1, winnings: 0, payIn: 30, profit: -30, profitPerGame: -30)
                            .VerifyPlayerInList("Yokozuna", gamesPlayed: 1, winnings: 310, payIn: 150, profit: 160, profitPerGame: 160);
        }
    }
}
