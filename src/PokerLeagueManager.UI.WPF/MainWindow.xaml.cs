using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Commands;

namespace PokerLeagueManager.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var svc = Resolver.Container.Resolve<ICommandService>();

            var cmdFactory = Resolver.Container.Resolve<ICommandFactory>();
            var cmd = cmdFactory.Create<EnterGameResultsCommand>();
            cmd.GameDate = DateTime.Parse("15-Oct-2012");
            cmd.GameId = Guid.NewGuid();

            svc.ExecuteCommand(cmd);
        }
    }
}
