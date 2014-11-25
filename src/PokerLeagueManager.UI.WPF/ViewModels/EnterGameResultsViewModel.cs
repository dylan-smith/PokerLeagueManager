using System;
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
    public class EnterGameResultsViewModel : BaseViewModel, INotifyPropertyChanged, IEnterGameResultsViewModel
    {
        private ObservableCollection<EnterGameResultsCommand.GamePlayer> _playerCommands;

        public EnterGameResultsViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
            : base(commandService, queryService, mainWindow, logger)
        {
            ResetPlayerCommands();

            AddPlayerCommand = new RelayCommand(x => this.AddPlayer(), x => this.CanAddPlayer());
            SaveGameCommand = new RelayCommand(x => this.SaveGame(), x => this.CanSaveGame());
            CancelCommand = new RelayCommand(x => this.Cancel());

            ClearNewPlayer();

            Height = 400;
            WindowTitle = "Enter Game Results";
        }

        public DateTime? GameDate { get; set; }

        public string NewPlayerName { get; set; }

        public string NewPlacing { get; set; }

        public string NewWinnings { get; set; }

        private Guid _gameId;

        public Guid GameId
        {
            get
            {
                return _gameId;
            }

            set
            {
                _gameId = value;
                var gameResults = _QueryService.GetGameResults(_gameId).First();
                GameDate = gameResults.GameDate;
                OnPropertyChanged("GameDate");

                foreach (var player in gameResults.Players)
                {
                    _playerCommands.Add(new EnterGameResultsCommand.GamePlayer() { PlayerName = player.PlayerName, Placing = player.Placing, Winnings = player.Winnings });
                }
            }
        }

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

        public System.Windows.Input.ICommand CancelCommand { get; set; }

        private void ResetPlayerCommands()
        {
            _playerCommands = new ObservableCollection<EnterGameResultsCommand.GamePlayer>();
            _playerCommands.CollectionChanged += (x, y) => OnPropertyChanged("Players");
        }

        private bool CanSaveGame()
        {
            return GameDate != null && _gameId == default(Guid);
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

            var commandResult = ExecuteCommand(gameCommand);

            if (commandResult)
            {
                Cancel();
            }
        }

        private void Cancel()
        {
            _MainWindow.ShowView(Resolver.Container.Resolve<IViewGamesListView>());
        }

        private void ClearNewPlayer()
        {
            this.NewPlayerName = string.Empty;
            this.NewPlacing = string.Empty;
            this.NewWinnings = "0";

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

            int temp;

            if (!int.TryParse(NewPlacing, out temp))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(NewWinnings) && !int.TryParse(NewWinnings, out temp))
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

            newPlayer.PlayerName = NewPlayerName;
            newPlayer.Placing = int.Parse(NewPlacing);

            if (string.IsNullOrWhiteSpace(NewWinnings))
            {
                newPlayer.Winnings = 0;
            }
            else
            {
                newPlayer.Winnings = int.Parse(NewWinnings);
            }

            _playerCommands.Add(newPlayer);

            ClearNewPlayer();
        }
    }
}
