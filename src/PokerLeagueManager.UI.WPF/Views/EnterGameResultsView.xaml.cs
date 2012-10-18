using PokerLeagueManager.UI.WPF.ViewModels;
using System.Windows.Controls;

namespace PokerLeagueManager.UI.WPF.Views
{
    public partial class EnterGameResultsView : UserControl
    {
        public EnterGameResultsView(EnterGameResultsViewModel viewModel)
        {
            // TODO: Support resizing
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
