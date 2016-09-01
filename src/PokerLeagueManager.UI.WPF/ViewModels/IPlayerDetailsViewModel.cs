using System.Collections.Generic;
using System.ComponentModel;

namespace PokerLeagueManager.UI.Wpf.ViewModels
{
    public interface IPlayerDetailsViewModel : INotifyPropertyChanged
    {
        System.Windows.Input.ICommand CloseCommand { get; set; }

        string PlayerName { get; set; }

        IEnumerable<string> Games { get; }
    }
}
