using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using log4net;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.UI.Wpf.Infrastructure;
using PokerLeagueManager.UI.Wpf.Views;

namespace PokerLeagueManager.UI.Wpf
{
    public partial class MainWindow : Window, IMainWindow
    {
        private readonly ILog _logger;

        public MainWindow()
        {
            InitializeComponent();

            Application.Current.DispatcherUnhandledException += GlobalExceptionHandler;
            Resolver.Container.RegisterInstance<IMainWindow>(this);
            _logger = Resolver.Container.Resolve<ILog>();

            ShowView(Resolver.Container.Resolve<GamesListView>());
        }

        public void ShowView(object view)
        {
            Content = view;

            var viewControl = (UserControl)view;
            MinHeight = viewControl.MinHeight + 30;
            MinWidth = viewControl.MinWidth + 30;
            MaxHeight = viewControl.MaxHeight + 30;
            MaxWidth = viewControl.MaxWidth + 30;

            var viewModel = (BaseViewModel)viewControl.DataContext;
            Height = viewModel.Height + 30;
            Width = viewModel.Width + 30;
            Title = viewModel.WindowTitle;
        }

        public void ShowWarning(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public MessageBoxResult ShowConfirmation(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }

        public void SetWidth(int width)
        {
            Width = width;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }

        public void SetMinWidth(int minWidth)
        {
            MinWidth = minWidth;
        }

        public void SetMinHeight(int minHeight)
        {
            MinHeight = minHeight;
        }

        public void SetMaxWidth(int maxWidth)
        {
            MaxWidth = maxWidth;
        }

        public void SetMaxHeight(int maxHeight)
        {
            MaxHeight = maxHeight;
        }

        public void SetWindowTitle(string title)
        {
            Title = title;
        }

        private void GlobalExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Fatal("Unhandled Exception", e.Exception);
            MessageBox.Show("An unexpected error has occurred. The details have been logged. The application will now shutdown.", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
