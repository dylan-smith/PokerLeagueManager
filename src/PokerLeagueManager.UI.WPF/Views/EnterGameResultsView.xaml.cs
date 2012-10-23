using System.Windows.Controls;
using PokerLeagueManager.UI.Wpf.ViewModels;

namespace PokerLeagueManager.UI.Wpf.Views
{
    public partial class EnterGameResultsView : UserControl
    {
        public EnterGameResultsView(EnterGameResultsViewModel viewModel)
        {
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
