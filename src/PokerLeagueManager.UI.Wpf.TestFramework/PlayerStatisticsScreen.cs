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
    }
}
