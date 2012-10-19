using System.Windows.Controls;
using PokerLeagueManager.UI.WPF.ViewModels;

namespace PokerLeagueManager.UI.WPF.Views
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
