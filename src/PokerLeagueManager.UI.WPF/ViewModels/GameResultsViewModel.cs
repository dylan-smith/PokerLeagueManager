using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class GameResultsViewModel : BaseViewModel, INotifyPropertyChanged, IGameResultsViewModel
    {
        private Guid _gameId;

        public GameResultsViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            CloseCommand = new RelayCommand(x => Close());

            Height = 400;
            WindowTitle = "View Game Results";
        }

        public IEnumerable<string> Players { get; private set; }

        public string GameDate { get; set; }

        public Guid GameId
        {
            get
            {
                return _gameId;
            }

            set
            {
                _gameId = value;

                var gameResults = QueryService.GetGameResults(_gameId);

                GameDate = gameResults.GameDate.ToString("d-MMM-yyyy");
                OnPropertyChanged("GameDate");

                Players = gameResults.Players.OrderBy(p => p.Placing)
                                             .Select(p => string.Format("{0} - {1} [Win: ${2}] [Pay: ${3}]", p.Placing, p.PlayerName, p.Winnings, p.PayIn));
                OnPropertyChanged("Players");
            }
        }

        public System.Windows.Input.ICommand CloseCommand { get; set; }

        private void Close()
        {
            MainWindow.ShowView(Resolver.Container.Resolve<IGamesListView>());
        }
    }
}
