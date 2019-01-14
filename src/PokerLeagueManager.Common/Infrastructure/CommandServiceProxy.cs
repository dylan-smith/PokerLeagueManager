using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class CommandServiceProxy : ICommandService, IDisposable
    {
        private readonly HttpClient _commandClient;
        private bool _disposedValue = false;

        public CommandServiceProxy()
        {
            var commandUrl = ConfigurationManager.AppSettings["CommandServiceUrl"];

            _commandClient = new HttpClient();
            _commandClient.BaseAddress = new Uri(commandUrl);
            _commandClient.DefaultRequestHeaders.Accept.Clear();
            _commandClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetUrl(string commandUrl)
        {
            _commandClient.BaseAddress = new Uri(commandUrl);
        }

        public void ExecuteCommand(ICommand command)
        {
            var actionName = GetActionName(command);
            var task = _commandClient.PostAsJsonAsync($"/{actionName}", command);
            task.Wait();
            var response = task.Result;
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _commandClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        private object GetActionName(ICommand command)
        {
            var commandName = command.GetType().Name;
            return commandName.Substring(0, commandName.Length - "Command".Length);
        }
    }
}
