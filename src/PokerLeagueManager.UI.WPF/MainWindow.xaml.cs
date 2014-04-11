using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands.Infrastructure;
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

        public void ShowWarning(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void GlobalExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
