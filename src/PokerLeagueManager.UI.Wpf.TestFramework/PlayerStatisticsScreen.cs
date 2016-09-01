using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class PlayerStatisticsScreen : BaseScreen
    {
        public PlayerStatisticsScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public PlayerStatisticsScreen VerifyPlayerInList(string playerName)
        {
            TakeScreenshot();
            Assert.IsTrue(FindPlayerListItem(playerName).TryFind(), playerName);
            return this;
        }

        public PlayerStatisticsScreen VerifyPlayerInList(string playerName, int gamesPlayed)
        {
            TakeScreenshot();

            var listItem = FindPlayerListItem(playerName);
            Assert.IsTrue(listItem.TryFind());
            Assert.IsTrue(listItem.DisplayText.Contains("Games Played: " + gamesPlayed.ToString()));

            return this;
        }

        public PlayerStatisticsScreen VerifyPlayerInList(string playerName, int gamesPlayed, int winnings, int payIn, int profit, double profitPerGame)
        {
            TakeScreenshot();

            var listItem = FindPlayerListItem(playerName);
            Assert.IsTrue(listItem.TryFind());
            Assert.IsTrue(listItem.DisplayText.Contains("Games Played: " + gamesPlayed.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Games Played: " + gamesPlayed.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Winnings: $" + winnings.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Winnings: $" + winnings.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Pay In: $" + payIn.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Pay In: $" + payIn.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Profit: $" + profit.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Profit: $" + profit.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Profit Per Game: $" + profitPerGame.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Profit Per Game: $" + profitPerGame.ToString() + "]");

            return this;
        }

        public PlayerStatisticsScreen VerifyPlayerListCount(int expectedCount)
        {
            TakeScreenshot();

            Assert.AreEqual(expectedCount, FindAllPlayerListItems().Count);
            return this;
        }

        public PlayerGamesScreen DoubleClickPlayer(string playerName)
        {
            var item = FindPlayerListItem(playerName);
            Mouse.DoubleClick(item);

            return new PlayerGamesScreen(App);
        }

        private WpfListItem FindPlayerListItem(string playerName)
        {
            var playersList = new WpfList(App);
            playersList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "PlayersListBox");

            var ctl = new WpfListItem(playersList);
            ctl.SearchProperties.Add(WpfListItem.PropertyNames.Name, playerName, PropertyExpressionOperator.Contains);
            return ctl;
        }

        private UITestControlCollection FindAllPlayerListItems()
        {
            var playersList = new WpfList(App);
            playersList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "PlayersListBox");

            var ctl = new WpfListItem(playersList);
            return ctl.FindMatchingControls();
        }
    }
}
