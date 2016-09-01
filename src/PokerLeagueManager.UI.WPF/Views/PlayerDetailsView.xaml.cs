using System.Windows.Controls;
using PokerLeagueManager.UI.Wpf.ViewModels;

namespace PokerLeagueManager.UI.Wpf.Views
{
    public partial class PlayerDetailsView : UserControl, IPlayerDetailsView
    {
        private IPlayerDetailsViewModel _viewModel;

        public PlayerDetailsView(IPlayerDetailsViewModel viewModel)
        {
            this.DataContext = viewModel;
            _viewModel = viewModel;

            InitializeComponent();
        }

        public string PlayerName
        {
            get { return _viewModel.PlayerName; }

            set { _viewModel.PlayerName = value; }
        }
    }
}
