using Microsoft.Practices.Unity;
using PokerLeagueManager.UI.WPF.Views;
using System.Windows;

namespace PokerLeagueManager.UI.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Content = Resolver.Container.Resolve<EnterGameResultsView>();
        }
    }
}
