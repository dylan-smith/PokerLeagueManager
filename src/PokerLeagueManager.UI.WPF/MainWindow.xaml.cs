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

            Application.Current.DispatcherUnhandledException += GlobalExceptionHandler;
            Resolver.Container.RegisterInstance<IMainWindow>(this);

            ShowView(Resolver.Container.Resolve<ViewGamesListView>());
        }

        public void ShowView(object view)
        {
            this.Content = view;
        }

        private void GlobalExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
