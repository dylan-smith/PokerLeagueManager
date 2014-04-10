using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace WCFExceptionHandling.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var svc = new CommandServiceProxy();

            try
            {
                var result = svc.ExecuteCommand();
                MessageBox.Show(result.ToString());
            }
            catch (Exception ex)
            {
                var foo = (FaultException)ex;
                MessageBox.Show(ex.GetType().ToString());
            }
        }
    }
}
