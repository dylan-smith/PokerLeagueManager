using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class GamesListViewModel : BaseViewModel, INotifyPropertyChanged, IGamesListViewModel
    {
        private ObservableCollection<GetGamesListDto> _games;

        public GamesListViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            var gamesList = new GetGamesListQuery().Execute(QueryService);
            _games = new ObservableCollection<GetGamesListDto>(gamesList);

            PlayersCommand = new RelayCommand(x => NavigateToPlayersView());
            AddGameCommand = new RelayCommand(x => AddGame());
            DeleteGameCommand = new RelayCommand(x => DeleteGame(), x => CanDeleteGame());
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

        public System.Windows.Input.ICommand PlayersCommand { get; set; }

        public System.Windows.Input.ICommand AddGameCommand { get; set; }

        public System.Windows.Input.ICommand DeleteGameCommand { get; set; }

        public System.Windows.Input.ICommand GameDoubleClickCommand { get; set; }

        private bool CanDeleteGame()
        {
            return _games.Count > 0 && SelectedGameIndex >= 0;
        }

        private void NavigateToPlayersView()
        {
            var view = Resolver.Container.Resolve<IPlayerStatisticsView>();
            MainWindow.ShowView(view);
        }

        private void AddGame()
        {
            var view = Resolver.Container.Resolve<IEnterGameResultsView>();
            MainWindow.ShowView(view);
        }

        private void DeleteGame()
        {
            var gameId = GetSelectedGame().GameId;
            CommandService.ExecuteCommand(new DeleteGameCommand() { GameId = gameId });

            var gamesList = new GetGamesListQuery().Execute(QueryService);
            _games = new ObservableCollection<GetGamesListDto>(gamesList);
            OnPropertyChanged("Games");
        }

        private void GameDoubleClick()
        {
            if (_games.Count() == 0 || SelectedGameIndex < 0)
            {
                return;
            }

            var view = Resolver.Container.Resolve<IGameResultsView>();
            view.GameId = GetSelectedGame().GameId;
            view.GameDate = GetSelectedGame().GameDate;
            MainWindow.ShowView(view);
        }

        private GetGamesListDto GetSelectedGame()
        {
            return _games.OrderByDescending(g => g.GameDate).ElementAt(SelectedGameIndex);
        }
    }
}
