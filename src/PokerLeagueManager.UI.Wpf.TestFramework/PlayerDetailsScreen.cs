using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class PlayerDetailsScreen : BaseScreen
    {
        public PlayerDetailsScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public override void VerifyScreen()
        {
            TakeScreenshot();
            RenamePlayerButton.Find();
        }

        public PlayerDetailsScreen VerifyDuplicatePlayerWarning()
        {
            TakeScreenshot();
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("CANNOT ADD THE SAME PLAYER"), "Did not contain CANNOT ADD THE SAME PLAYER: " + ActionFailedMessage.DisplayText.ToUpper());
            return this;
        }

        public PlayerDetailsScreen DismissWarningDialog()
        {
            Mouse.Click(ActionFailedOkButton);
            return this;
        }

        public PlayerDetailsScreen ClickConfirmMerge()
        {
            Mouse.Click(ConfirmMergeOkButton);
            return this;
        }

        public PlayerDetailsScreen EnterNewPlayerName(string newPlayerName)
        {
            NewPlayerNameTextBox.Text = newPlayerName;
            return this;
        }

        public PlayerDetailsScreen ClickRenamePlayer()
        {
            Mouse.Click(RenamePlayerButton);
            return this;
        }

        public PlayerDetailsScreen VerifyPlayerName(string playerName)
        {
            Assert.AreEqual(playerName, PlayerNameTextBox.Text);
            return this;
        }

        public PlayerDetailsScreen VerifyGameCount(int expectedCount)
        {
            var allGameListItems = FindGameListItems();
            Assert.AreEqual(expectedCount, allGameListItems.Count);
            return this;
        }

        public PlayerDetailsScreen VerifyGameInList(DateTime gameDate, int placing, int winnings, int payIn)
        {
            TakeScreenshot();

            var listItem = FindGameListItem(gameDate.ToString("dd-MMM-yyyy"));
            Assert.IsTrue(listItem.TryFind(), "No Game could be found with a matching date [" + gameDate.ToString("dd-MMM-yyyy") + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Placing: " + placing.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Placing: " + placing.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Winnings: $" + winnings.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Winnings: $" + winnings.ToString() + "]");
            Assert.IsTrue(listItem.DisplayText.Contains("Pay In: $" + payIn.ToString()), "[" + listItem.DisplayText + "] does not contain [" + "Pay In: $" + payIn.ToString() + "]");

            return this;
        }

        public PlayerStatisticsScreen ClickClose()
        {
            Mouse.Click(CloseButton);
            return new PlayerStatisticsScreen(App);
        }

        private UITestControlCollection FindGameListItems()
        {
            var gameList = new WpfList(App);
            gameList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "GamesListBox");

            var ctl = new WpfListItem(gameList);
            return ctl.FindMatchingControls();
        }

        private WpfListItem FindGameListItem(string gameDate)
        {
            var gameList = new WpfList(App);
            gameList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "GamesListBox");

            var ctl = new WpfListItem(gameList);
            ctl.SearchProperties.Add(WpfListItem.PropertyNames.Name, gameDate, PropertyExpressionOperator.Contains);
            return ctl;
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

        private WinWindow ConfirmMergeMessageBox
        {
            get
            {
                var ctl = new WinWindow();
                ctl.SearchProperties.Add(WinWindow.PropertyNames.Name, "Confirm Player Merge?");
                return ctl;
            }
        }

        private WinButton ConfirmMergeOkButton
        {
            get
            {
                var ctl = new WinButton(ConfirmMergeMessageBox);
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
