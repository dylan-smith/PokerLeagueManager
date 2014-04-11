using System;
using System.ComponentModel;
using System.ServiceModel;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;

namespace PokerLeagueManager.UI.Wpf.Infrastructure
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow)
        {
            _MainWindow = mainWindow;
            _CommandService = commandService;
            _QueryService = queryService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected IMainWindow _MainWindow { get; set; }

        protected ICommandService _CommandService { get; set; }

        protected IQueryService _QueryService { get; set; }

        protected bool ExecuteCommand(ICommand command)
        {
            try
            {
                _CommandService.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                throw new ArgumentException("Invalid property name", propertyName);
            }
        }

        private bool HandleException(Exception ex)
        {
            var actionSucceeded = false;

            var fault = ex as FaultException<ExceptionDetail>;

            if (fault != null)
            {
                if (fault.Detail.Type.StartsWith("PokerLeagueManager"))
                {
                    if (fault.Detail.Type.Contains("PublishEventFailedException"))
                    {
                        _MainWindow.ShowWarning("Action Succeeded", fault.Detail.Message);
                        actionSucceeded = true;
                    }
                    else
                    {
                        _MainWindow.ShowWarning("Action Failed", fault.Detail.Message);
                    }
                }
                else
                {
                    _MainWindow.ShowError("Action Failed", fault.Detail.Message);
                }
            }
            else
            {
                _MainWindow.ShowError("Action Failed", ex.Message);
            }

            return actionSucceeded;
        }
    }
}
