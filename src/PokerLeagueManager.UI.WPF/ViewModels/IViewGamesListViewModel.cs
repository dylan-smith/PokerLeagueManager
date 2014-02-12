using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public interface IViewGamesListViewModel : INotifyPropertyChanged
    {
        System.Windows.Input.ICommand AddGameCommand { get; set; }

        IEnumerable<string> Games { get; }
    }
}
