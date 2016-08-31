using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
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

        public PlayerGamesScreen VerifyDuplicatePlayerWarning()
        {
            TakeScreenshot();
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("CANNOT ADD THE SAME PLAYER"), "Did not contain CANNOT ADD THE SAME PLAYER: " + ActionFailedMessage.DisplayText.ToUpper());
            return this;
        }

        public PlayerGamesScreen DismissWarningDialog()
        {
            Mouse.Click(ActionFailedOkButton);
            return this;
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

        private WinWindow ActionFailedMessageBox
        {
            get
            {
                var ctl = new WinWindow();
                ctl.SearchProperties.Add(WinWindow.PropertyNames.Name, "Action Failed");
                return ctl;
            }
        }

        private WinText ActionFailedMessage
        {
            get
            {
                var ctl = new WinText(ActionFailedMessageBox);
                return ctl;
            }
        }

        private WinButton ActionFailedOkButton
        {
            get
            {
                var ctl = new WinButton(ActionFailedMessageBox);
                ctl.SearchProperties.Add(WinButton.PropertyNames.Name, "OK");
                return ctl;
            }
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
