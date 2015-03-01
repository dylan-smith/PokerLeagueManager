using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class PlayerGamesViewModel : BaseViewModel, INotifyPropertyChanged, IPlayerGamesViewModel
    {
        private ObservableCollection<GetPlayerGamesDto> _games;

        private string _playerName;

        public PlayerGamesViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            CloseCommand = new RelayCommand(x => Close());

            Height = 400;
            Width = 385;
            WindowTitle = "View Player Games";
        }

        public string PlayerName
        {
            get
            {
                return _playerName;
            }

            set
            {
                _playerName = value;

                _games = new ObservableCollection<GetPlayerGamesDto>(_QueryService.GetPlayerGames(_playerName));
            }
        }

        public IEnumerable<string> Games
        {
            get
            {
                return _games.OrderByDescending(g => g.GameDate)
                             .Select(g => string.Format("{0} - Placing: {1} - Winnings: ${2} - Pay In: ${3}", g.GameDate.ToString("dd-MMM-yyyy"), g.Placing, g.Winnings, g.PayIn));
            }
        }

        public System.Windows.Input.ICommand CloseCommand { get; set; }

        private void Close()
        {
            var view = Resolver.Container.Resolve<IPlayerStatisticsView>();
            _MainWindow.ShowView(view);
        }
    }
}
