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
            Assert.IsTrue(listItem.TryFind(), playerName);
            Assert.IsTrue(listItem.DisplayText.Contains("Games Played: " + gamesPlayed.ToString()));

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
