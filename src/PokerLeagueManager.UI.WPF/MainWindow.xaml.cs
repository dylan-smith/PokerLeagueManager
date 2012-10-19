using System.Windows;
using Microsoft.Practices.Unity;
using PokerLeagueManager.UI.WPF.Views;

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
