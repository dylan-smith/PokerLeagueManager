﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class EnterGameResultsScreen : BaseScreen
    {
        public EnterGameResultsScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public EnterGameResultsScreen EnterGameDate(string gameDate)
        {
            GameDatePicker.DateAsString = gameDate;
            return this;
        }

        public EnterGameResultsScreen EnterPlayerName(string playerName)
        {
            PlayerNameTextBox.Text = playerName;
            return this;
        }

        public EnterGameResultsScreen EnterPlacing(string playerPlacing)
        {
            PlacingTextBox.Text = playerPlacing;
            return this;
        }

        public EnterGameResultsScreen EnterWinnings(string playerWinnings)
        {
            WinningsTextBox.Text = playerWinnings;
            return this;
        }

        public EnterGameResultsScreen ClickAddPlayer()
        {
            Mouse.Click(AddPlayerButton);
            return this;
        }

        public ViewGamesListScreen ClickSaveGame()
        {
            Mouse.Click(SaveGameButton);
            return new ViewGamesListScreen(App);
        }

        public ViewGamesListScreen ClickCancel()
        {
            Mouse.Click(CancelButton);
            return new ViewGamesListScreen(App);
        }

        public EnterGameResultsScreen DismissWarningDialog()
        {
            Mouse.Click(ActionFailedOkButton);
            return this;
        }

        public void VerifyDuplicateGameDateWarning()
        {
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("DATE"), "Did not contain DATE: " + ActionFailedMessage.DisplayText.ToUpper());
        }

        public void VerifyNotEnoughPlayersWarning()
        {
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("2 PLAYERS"), "Did not contain 2 PLAYERS: " + ActionFailedMessage.DisplayText.ToUpper());
        }

        public void VerifyDuplicatePlayerWarning()
        {
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("CANNOT ADD THE SAME PLAYER"), "Did not contain CANNOT ADD THE SAME PLAYER: " + ActionFailedMessage.DisplayText.ToUpper());
        }

        public void VerifySaveGameIsDisabled()
        {
            Assert.IsFalse(SaveGameButton.Enabled);
        }

        public void VerifyInvalidWinningsWarning()
        {
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("WINNINGS"), "Did not contain WINNINGS: " + ActionFailedMessage.DisplayText.ToUpper());
        }

        public void VerifyInvalidPlacingWarning()
        {
            Assert.IsTrue(ActionFailedMessage.DisplayText.ToUpper().Contains("PLACING"), "Did not contain PLACING: " + ActionFailedMessage.DisplayText.ToUpper());
        }

        public override void VerifyScreen()
        {
            TakeScreenshot();
            AddPlayerButton.Find();
        }

        private WpfDatePicker GameDatePicker
        {
            get
            {
                var ctl = new WpfDatePicker(App);
                ctl.SearchProperties.Add(WpfDatePicker.PropertyNames.AutomationId, "GameDatePicker");
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

        private WpfEdit PlacingTextBox
        {
            get
            {
                var ctl = new WpfEdit(App);
                ctl.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "PlacingTextBox");
                return ctl;
            }
        }

        private WpfEdit WinningsTextBox
        {
            get
            {
                var ctl = new WpfEdit(App);
                ctl.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "WinningsTextBox");
                return ctl;
            }
        }

        private WpfButton AddPlayerButton
        {
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "AddPlayerButton");
                return ctl;
            }
        }

        private WpfButton SaveGameButton
        {
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "SaveGameButton");
                return ctl;
            }
        }

        private WpfButton CancelButton
        {
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "CancelButton");
                return ctl;
            }
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

        private WinButton ActionFailedOkButton
        {
            get
            {
                var ctl = new WinButton(ActionFailedMessageBox);
                ctl.SearchProperties.Add(WinButton.PropertyNames.Name, "OK");
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

        public EnterGameResultsScreen VerifyPlayerList(params string[] expectedPlayers)
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
    }
}
