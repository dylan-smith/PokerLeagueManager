using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class CommandServiceProxy : ClientBase<ICommandService>, ICommandService
    {
        public void ExecuteCommand(ICommand command)
        {
            base.Channel.ExecuteCommand(command);
        }
    }
}
