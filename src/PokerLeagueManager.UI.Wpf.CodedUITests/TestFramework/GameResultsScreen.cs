using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.UI.Wpf.CodedUITests.Infrastructure;

namespace PokerLeagueManager.UI.Wpf.CodedUITests.TestFramework
{
    public class GameResultsScreen : BaseScreen
    {
        public GameResultsScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public GamesListScreen ClickClose()
        {
            Mouse.Click(CloseButton);
            return new GamesListScreen(App);
        }

        public GameResultsScreen VerifyGameDate(string gameDate)
        {
            Assert.AreEqual(gameDate, GameDateTextBox.Text);
            return this;
        }

        public override void VerifyScreen()
        {
            TakeScreenshot();
            CloseButton.Find();
        }

        private WpfButton CloseButton
        {
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "CloseButton");
                return ctl;
            }
        }

        public GameResultsScreen VerifyPlayerList(params string[] expectedPlayers)
        {
            var actualPlayers = PlayerListItems;

            foreach (var p in expectedPlayers)
            {
                Assert.IsTrue(actualPlayers.Any(x => x.Name == p), string.Format("{0} not found in list", p));
            }

            return this;
        }

        private UITestControlCollection PlayerListItems
        {
            get
            {
                var list = new WpfList(App);
                list.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "PlayersListBox");

                var items = new WpfListItem(list);
                return items.FindMatchingControls();
            }
        }

        private WpfEdit GameDateTextBox
        {
            get
            {
                var ctl = new WpfEdit(App);
                ctl.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "GameDateTextBox");
                return ctl;
            }
        }
    }
}
