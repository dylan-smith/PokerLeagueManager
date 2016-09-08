using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public interface IGameResultsViewModel : INotifyPropertyChanged
    {
        System.Windows.Input.ICommand CloseCommand { get; set; }

        DateTime GameDate { get; set; }

        string GameDateText { get; }

        IEnumerable<string> Players { get; }

        Guid GameId { get; set; }
    }
}
