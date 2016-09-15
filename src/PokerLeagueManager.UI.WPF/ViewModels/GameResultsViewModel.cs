using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class GameResultsViewModel : BaseViewModel, INotifyPropertyChanged, IGameResultsViewModel
    {
        private Guid _gameId;
        private DateTime _gameDate;

        public GameResultsViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            CloseCommand = new RelayCommand(x => Close());

            Height = 400;
            WindowTitle = "View Game Results";
        }

        public IEnumerable<string> Players { get; private set; }

        public DateTime GameDate
        {
            get
            {
                return _gameDate;
            }

            set
            {
                _gameDate = value;
                OnPropertyChanged("GameDateText");
            }
        }

        public string GameDateText
        {
            get
            {
                return GameDate.ToString("d-MMM-yyyy");
            }
        }

        public Guid GameId
        {
            get
            {
                return _gameId;
            }

            set
            {
                _gameId = value;

                var gamePlayers = new GetGamePlayersQuery(_gameId).Execute(QueryService);

                Players = gamePlayers.OrderBy(p => p.Placing)
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
