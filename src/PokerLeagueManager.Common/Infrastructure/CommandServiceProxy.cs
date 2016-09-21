using System.Configuration;
using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class CommandServiceProxy : ClientBase<ICommandService>, ICommandService
    {
        public CommandServiceProxy()
        {
            var commandUrl = ConfigurationManager.AppSettings["CommandServiceUrl"];
            base.Endpoint.Address = new EndpointAddress(commandUrl);
        }

        public void ExecuteCommand(ICommand command)
        {
            base.Channel.ExecuteCommand(command);
        }
    }
}
