using System.Windows;
using Microsoft.Practices.Unity;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Content = Resolver.Container.Resolve<ViewGamesListView>();
            //// this.Content = Resolver.Container.Resolve<EnterGameResultsView>();
        }
    }
}
