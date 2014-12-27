using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class ViewGamesListViewModel : BaseViewModel, INotifyPropertyChanged, IViewGamesListViewModel
    {
        private List<GetGamesListDto> _games;

        public ViewGamesListViewModel(IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(null, queryService, mainWindow, logger)
        {
            _games = new List<GetGamesListDto>(_QueryService.GetGamesList());

            AddGameCommand = new RelayCommand(x => AddGame());
            GameDoubleClickCommand = new RelayCommand(x => GameDoubleClick());

            Height = 400;
            Width = 385;
            WindowTitle = "View Games";
        }

        public IEnumerable<string> Games
        {
            get
            {
                return _games.OrderByDescending(g => g.GameDate)
                             .Select(g => string.Format("{0} - {1} [${2}]", g.GameDate.ToString("dd-MMM-yyyy"), g.Winner, g.Winnings));
            }
        }

        public int SelectedGameIndex { get; set; }

        public System.Windows.Input.ICommand AddGameCommand { get; set; }

        public System.Windows.Input.ICommand GameDoubleClickCommand { get; set; }

        private void AddGame()
        {
            var view = Resolver.Container.Resolve<IEnterGameResultsView>();
            _MainWindow.ShowView(view);
        }

        private void GameDoubleClick()
        {
            var selectedGame = _games.OrderByDescending(g => g.GameDate).ElementAt(SelectedGameIndex);
            var view = Resolver.Container.Resolve<IViewGameResultsView>();
            view.GameId = selectedGame.GameId;
            _MainWindow.ShowView(view);
        }
    }
}
