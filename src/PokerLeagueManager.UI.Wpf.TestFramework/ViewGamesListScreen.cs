using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerLeagueManager.UI.Wpf.TestFramework
{
    public class ViewGamesListScreen : BaseScreen
    {
        public ViewGamesListScreen(ApplicationUnderTest app)
            : base(app)
        {
        }

        public EnterGameResultsScreen ClickAddGame()
        {
            Mouse.Click(AddGameButton);
            return new EnterGameResultsScreen(App);
        }

        public void VerifyGameInList(string gameDescription)
        {
            Assert.IsTrue(FindGameListItem(gameDescription).TryFind(), gameDescription);
        }

        public string FindUnusedGameDate()
        {
            var allGames = GetAllGameListItems();

            var usedDates = allGames.Select(x => x.Name.Substring(0, x.Name.IndexOf(" - ")));

            var randomDate = GenerateRandomDate(1900, 2100);

            while (usedDates.Any(x => x == randomDate.ToString("dd-MMM-yyyy")))
            {
                randomDate = GenerateRandomDate(1900, 2100);
            }

            return randomDate.ToString("dd-MMM-yyyy");
        }

        public string FindUsedGameDate()
        {
            var allGames = GetAllGameListItems();

            var usedDates = allGames.Select(x => x.Name.Substring(0, x.Name.IndexOf(" - ")));

            return usedDates.First();
        }

        private DateTime GenerateRandomDate(int minYear, int maxYear)
        {
            var randomDays = GenerateRandomInteger((maxYear - minYear) * 365);

            var result = new DateTime(minYear, 1, 1);

            return result.AddDays(randomDays);
        }

        private int GenerateRandomInteger(int max)
        {
            var rnd = new Random();
            return rnd.Next(max);
        }

        private WpfButton AddGameButton
        { 
            get
            {
                var ctl = new WpfButton(App);
                ctl.SearchProperties.Add(WpfButton.PropertyNames.AutomationId, "AddGameButton");
                return ctl;
            }
        }

        private WpfListItem FindGameListItem(string gameDescription)
        {
            var gamesList = new WpfList(App);
            gamesList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "GamesListBox");

            var ctl = new WpfListItem(gamesList);
            ctl.SearchProperties.Add(WpfListItem.PropertyNames.Name, gameDescription, PropertyExpressionOperator.Contains);
            return ctl;
        }

        private UITestControlCollection GetAllGameListItems()
        {
            var gamesList = new WpfList(App);
            gamesList.SearchProperties.Add(WpfList.PropertyNames.AutomationId, "GamesListBox");

            var ctl = new WpfListItem(gamesList);
            return ctl.FindMatchingControls();
        }
    }
}
