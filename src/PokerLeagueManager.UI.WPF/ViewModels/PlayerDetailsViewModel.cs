using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class PlayerDetailsViewModel : BaseViewModel, INotifyPropertyChanged, IPlayerDetailsViewModel
    {
        private string _playerName;

        public PlayerDetailsViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            CloseCommand = new RelayCommand(x => Close());
            RenamePlayerCommand = new RelayCommand(x => RenamePlayer(), x => CanRenamePlayer());

            Height = 400;
            Width = 385;
            WindowTitle = "View Player Games";
        }

        public IEnumerable<string> Games { get; set; }

        public string NewPlayerName { get; set; }

        public string PlayerName
        {
            get
            {
                return _playerName;
            }

            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");

                var games = QueryService.GetPlayerGames(_playerName);

                Games = games.OrderByDescending(g => g.GameDate)
                             .Select(g => string.Format("{0} - Placing: {1} - Winnings: ${2} - Pay In: ${3}", g.GameDate.ToString("dd-MMM-yyyy"), g.Placing, g.Winnings, g.PayIn));
                OnPropertyChanged("Games");
            }
        }

        public System.Windows.Input.ICommand CloseCommand { get; set; }

        public System.Windows.Input.ICommand RenamePlayerCommand { get; set; }

        private void Close()
        {
            var view = Resolver.Container.Resolve<IPlayerStatisticsView>();
            MainWindow.ShowView(view);
        }

        private void RenamePlayer()
        {
            var matchingPlayer = QueryService.GetPlayerByName(NewPlayerName);

            if (matchingPlayer != null)
            {
                var result = MainWindow.ShowConfirmation("Confirm Player Merge?", "There is an existing player with the same name [" + matchingPlayer.PlayerName + "]. If you continue with the rename the results of these 2 players will be merged.  This cannot be undone.  Do you wish to continue with the rename/merge?");

                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            var cmd = new RenamePlayerCommand();
            cmd.OldPlayerName = PlayerName;
            cmd.NewPlayerName = NewPlayerName;

            if (base.ExecuteCommand(cmd))
            {
                PlayerName = NewPlayerName;
            }
        }

        private bool CanRenamePlayer()
        {
            return !string.IsNullOrWhiteSpace(NewPlayerName);
        }
    }
}
