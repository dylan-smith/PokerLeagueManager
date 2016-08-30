using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class PlayerGamesScreen : BaseScreen
    {
        public PlayerGamesScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public override void VerifyScreen()
        {
            TakeScreenshot();
            RenamePlayerButton.Find();
        }

        public PlayerGamesScreen EnterNewPlayerName(string newPlayerName)
        {
            NewPlayerNameTextBox.Text = newPlayerName;
            return this;
        }

        public PlayerGamesScreen ClickRenamePlayer()
        {
            Mouse.Click(RenamePlayerButton);
            return this;
        }

        public PlayerGamesScreen VerifyPlayerName(string playerName)
        {
            Assert.AreEqual(playerName, PlayerNameTextBox.Text);
            return this;
        }

        public PlayerStatisticsScreen ClickClose()
        {
            Mouse.Click(CloseButton);
            return new PlayerStatisticsScreen(App);
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

        private WpfButton RenamePlayerButton
        {
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "RenamePlayerButton");
                return ctl;
            }
        }

        private WpfEdit PlayerNameTextBox
        {
            get
            {
                var ctl = new WpfEdit(App);
                ctl.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "PlayerNameTextBox");
                return ctl;
            }
        }

        private WpfEdit NewPlayerNameTextBox
        {
            get
            {
                var ctl = new WpfEdit(App);
                ctl.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "NewPlayerNameTextBox");
                return ctl;
            }
        }
    }
}
