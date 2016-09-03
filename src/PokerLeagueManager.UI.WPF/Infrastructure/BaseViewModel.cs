using System;
using System.ComponentModel;
using System.ServiceModel;
using log4net;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;

namespace PokerLeagueManager.UI.Wpf.Infrastructure
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(ICommandService commandService, IQueryService queryService, IMainWindow mainWindow, ILog logger)
        {
            MainWindow = mainWindow;
            CommandService = commandService;
            QueryService = queryService;
            Logger = logger;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Width { get; set; }

        public int Height { get; set; }

        public string WindowTitle { get; set; }

        protected IMainWindow MainWindow { get; private set; }

        protected ICommandService CommandService { get; private set; }

        protected IQueryService QueryService { get; private set; }

        protected ILog Logger { get; private set; }

        protected bool ExecuteCommand(ICommand command)
        {
            try
            {
                Logger.Info(string.Format("Executing Command {0} [{1}]", command.GetType().Name, command.CommandId.ToString()));
                CommandService.ExecuteCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
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
            var fault = ex as FaultException<ExceptionDetail>;

            if (fault != null)
            {
                if (fault.Detail.Type.StartsWith("PokerLeagueManager"))
                {
                    Logger.Warn(fault.Detail.Type);
                    Logger.Warn(fault.Detail.Message);

                    if (fault.Detail.Type.Contains("PublishEventFailedException"))
                    {
                        MainWindow.ShowWarning("Action Succeeded", fault.Detail.Message);
                        return true;
                    }
                    else
                    {
                        MainWindow.ShowWarning("Action Failed", fault.Detail.Message);
                    }
                }
                else
                {
                    Logger.Error(fault.Detail.Type);
                    Logger.Error(fault.Detail.Message);
                    MainWindow.ShowError("Action Failed", fault.Detail.Message);
                }
            }
            else
            {
                Logger.Error("Command Failed", ex);
                MainWindow.ShowError("Action Failed", ex.Message);
            }

            return false;
        }
    }
}
