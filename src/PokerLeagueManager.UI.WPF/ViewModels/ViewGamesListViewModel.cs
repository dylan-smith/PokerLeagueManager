using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class ViewGamesListViewModel : BaseViewModel, INotifyPropertyChanged, IViewGamesListViewModel
    {
        private ObservableCollection<GetGamesListDto> _games;

        public ViewGamesListViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            _games = new ObservableCollection<GetGamesListDto>(_QueryService.GetGamesList());

            AddGameCommand = new RelayCommand(x => AddGame());
            DeleteGameCommand = new RelayCommand(x => DeleteGame());
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

        public System.Windows.Input.ICommand DeleteGameCommand { get; set; }

        public System.Windows.Input.ICommand GameDoubleClickCommand { get; set; }

        private void AddGame()
        {
            var view = Resolver.Container.Resolve<IEnterGameResultsView>();
            _MainWindow.ShowView(view);
        }

        private void DeleteGame()
        {
            var gameId = GetSelectedGame().GameId;
            _CommandService.ExecuteCommand(new DeleteGameCommand() { GameId = gameId });

            // Could just manually remove the one item from the collection instead of re-querying
            _games = new ObservableCollection<GetGamesListDto>(_QueryService.GetGamesList());
        }

        private void GameDoubleClick()
        {
            var view = Resolver.Container.Resolve<IViewGameResultsView>();
            view.GameId = GetSelectedGame().GameId;
            _MainWindow.ShowView(view);
        }

        private GetGamesListDto GetSelectedGame()
        {
            return _games.OrderByDescending(g => g.GameDate).ElementAt(SelectedGameIndex);
        }
    }
}
