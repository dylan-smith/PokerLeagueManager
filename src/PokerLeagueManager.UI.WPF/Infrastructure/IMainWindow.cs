using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PokerLeagueManager.UI.Wpf.Infrastructure
{
    public interface IMainWindow
    {
        void ShowView(object view);

        void ShowWarning(string title, string message);

        void ShowError(string title, string message);
    }
}
