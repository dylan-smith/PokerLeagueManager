using System.Windows.Controls;
using PokerLeagueManager.UI.Wpf.ViewModels;

namespace PokerLeagueManager.UI.Wpf.Views
{
    public partial class GamesListView : UserControl, IGamesListView
    {
        public GamesListView(IGamesListViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
