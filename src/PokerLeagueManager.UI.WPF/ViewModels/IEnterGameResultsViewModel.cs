using PokerLeagueManager.Common.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PokerLeagueManager.UI.WPF.ViewModels
{
    public interface IEnterGameResultsViewModel : INotifyPropertyChanged
    {
        System.Windows.Input.ICommand AddPlayerCommand { get; set; }
        System.Windows.Input.ICommand SaveGameCommand { get; set; }

        DateTime? GameDate { get; set; }
        string NewPlacing { get; set; }
        string NewPlayerName { get; set; }
        string NewWinnings { get; set; }
        ObservableCollection<EnterGameResultsCommand.GamePlayer> Players { get; set; }
    }
}
