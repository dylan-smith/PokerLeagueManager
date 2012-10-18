using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.UI.WPF.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PokerLeagueManager.UI.WPF.ViewModels
{
    public class EnterGameResultsViewModel : BaseViewModel, INotifyPropertyChanged, IEnterGameResultsViewModel
    {
        // TODO: Implement IDataErrorInfo stuff and show validation warnings on the UI

        public DateTime? GameDate { get; set; }
        public string NewPlayerName { get; set; }
        public string NewPlacing { get; set; }
        public string NewWinnings { get; set; }

        public ObservableCollection<EnterGameResultsCommand.GamePlayer> Players { get; set; }
        
        public System.Windows.Input.ICommand AddPlayerCommand { get; set; }
        public System.Windows.Input.ICommand SaveGameCommand { get; set; }

        private ICommandService _commandService;

        public EnterGameResultsViewModel(ICommandService commandService)
        {
            _commandService = commandService;

            Players = new ObservableCollection<EnterGameResultsCommand.GamePlayer>();
            
            AddPlayerCommand = new RelayCommand(x => this.AddPlayer(), x => this.CanAddPlayer());
            SaveGameCommand = new RelayCommand(x => this.SaveGame(), x => this.CanSaveGame());
        }

        private bool CanSaveGame()
        {
            return GameDate != null;
        }

        private void SaveGame()
        {
            var gameCommand = new EnterGameResultsCommand();

            gameCommand.GameDate = this.GameDate.GetValueOrDefault();
            gameCommand.Players = this.Players;

            _commandService.ExecuteCommand(gameCommand);

            ClearScreen();
        }

        private void ClearScreen()
        {
            this.GameDate = null;
            this.Players = new ObservableCollection<EnterGameResultsCommand.GamePlayer>();

            OnPropertyChanged("GameDate");
            OnPropertyChanged("Players");

            ClearNewPlayer();
        }

        private void ClearNewPlayer()
        {
            this.NewPlayerName = string.Empty;
            this.NewPlacing = string.Empty;
            this.NewWinnings = string.Empty;

            OnPropertyChanged("NewPlayerName");
            OnPropertyChanged("NewPlacing");
            OnPropertyChanged("NewWinnings");
        }

        private bool CanAddPlayer()
        {
            if (string.IsNullOrWhiteSpace(NewPlayerName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewPlacing))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewWinnings))
            {
                return false;
            }

            int temp;

            if (!int.TryParse(NewPlacing, out temp))
            {
                return false;
            }

            if (!int.TryParse(NewWinnings, out temp))
            {
                return false;
            }

            return true;
        }

        private void AddPlayer()
        {
            if (!CanAddPlayer())
            {
                throw new Exception("AddPlayer should not ever be called if CanAddPlayer returns false");
            }

            var newPlayer = new EnterGameResultsCommand.GamePlayer();

            newPlayer.PlayerName = this.NewPlayerName;
            newPlayer.Placing = int.Parse(this.NewPlacing);
            newPlayer.Winnings = int.Parse(this.NewWinnings);

            this.Players.Add(newPlayer);

            ClearNewPlayer();
        }
    }
}
