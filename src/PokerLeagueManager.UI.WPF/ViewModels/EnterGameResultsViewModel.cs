using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PokerLeagueManager.Common.Commands;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.UI.Wpf.Infrastructure;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public class EnterGameResultsViewModel : BaseViewModel, INotifyPropertyChanged, IEnterGameResultsViewModel
    {
        private ICommandService _commandService;
        private ObservableCollection<EnterGameResultsCommand.GamePlayer> _playerCommands;

        public EnterGameResultsViewModel(ICommandService commandService)
        {
            _commandService = commandService;

            ResetPlayerCommands();

            AddPlayerCommand = new RelayCommand(x => this.AddPlayer(), x => this.CanAddPlayer());
            SaveGameCommand = new RelayCommand(x => this.SaveGame(), x => this.CanSaveGame());
        }

        public DateTime? GameDate { get; set; }

        public string NewPlayerName { get; set; }

        public string NewPlacing { get; set; }

        public string NewWinnings { get; set; }

        public IEnumerable<string> Players
        {
            get
            {
                return _playerCommands.OrderBy(p => p.Placing)
                                      .Select(p => string.Format("{0} - {1}", p.Placing, p.PlayerName) + 
                                                   (p.Winnings > 0 ? string.Format(" [${0}]", p.Winnings) : string.Empty));
            }
        }

        public System.Windows.Input.ICommand AddPlayerCommand { get; set; }

        public System.Windows.Input.ICommand SaveGameCommand { get; set; }

        private void ResetPlayerCommands()
        {
            _playerCommands = new ObservableCollection<EnterGameResultsCommand.GamePlayer>();
            _playerCommands.CollectionChanged += (x, y) => OnPropertyChanged("Players");
        }

        private bool CanSaveGame()
        {
            return GameDate != null;
        }

        private void SaveGame()
        {
            if (!CanSaveGame())
            {
                throw new InvalidOperationException("SaveGame should never be called if CanSaveGame returns false");
            }

            var gameCommand = new EnterGameResultsCommand();

            gameCommand.GameDate = this.GameDate.GetValueOrDefault();
            gameCommand.Players = _playerCommands;

            _commandService.ExecuteCommand(gameCommand);

            ClearScreen();
        }

        private void ClearScreen()
        {
            this.GameDate = null;

            ResetPlayerCommands();

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
                throw new InvalidOperationException("AddPlayer should never be called if CanAddPlayer returns false");
            }

            var newPlayer = new EnterGameResultsCommand.GamePlayer();

            newPlayer.PlayerName = this.NewPlayerName;
            newPlayer.Placing = int.Parse(this.NewPlacing);
            newPlayer.Winnings = int.Parse(this.NewWinnings);

            _playerCommands.Add(newPlayer);

            ClearNewPlayer();
        }
    }
}
