using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf
{
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Resolver.Container.RegisterInstance<IMainWindow>(this);

            ShowView(Resolver.Container.Resolve<ViewGamesListView>());
            //// ShowView(Resolver.Container.Resolve<EnterGameResultsView>());
        }

        public void ShowView(object view)
        {
            this.Content = view;
        }
    }
}
