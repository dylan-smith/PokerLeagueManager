using System.Windows.Controls;
using PokerLeagueManager.UI.Wpf.ViewModels;

namespace PokerLeagueManager.UI.Wpf.Views
{
    public partial class PlayerStatisticsView : UserControl, IPlayerStatisticsView
    {
        public PlayerStatisticsView(IPlayerStatisticsViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
