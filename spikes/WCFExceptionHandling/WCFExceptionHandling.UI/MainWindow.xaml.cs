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
            catch (FaultException<ExceptionDetail> ex)
            {
                HandleServiceException(ex.Detail);
            }
        }

        private void HandleServiceException(ExceptionDetail ex)
        {
            // should log the error here too
            if (ex.Type.StartsWith("WCFExceptionHandling."))
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(ex.Message, "Action Failed - Unexpected Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
