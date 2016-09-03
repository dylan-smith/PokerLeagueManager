using System;
using System.Windows.Controls;
using PokerLeagueManager.UI.Wpf.ViewModels;

namespace PokerLeagueManager.UI.Wpf.Views
{
    public partial class GameResultsView : UserControl, IGameResultsView
    {
        private IGameResultsViewModel _viewModel;

        public GameResultsView(IGameResultsViewModel viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;

            InitializeComponent();
        }

        public Guid GameId
        {
            get { return _viewModel.GameId; }

            set { _viewModel.GameId = value; }
        }
    }
}
