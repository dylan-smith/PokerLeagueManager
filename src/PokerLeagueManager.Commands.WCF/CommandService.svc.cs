using Microsoft.Practices.Unity;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands.Infrastructure;

namespace PokerLeagueManager.Commands.WCF
{
    public class CommandService : ICommandService
    {
        public void ExecuteCommand(ICommand command)
        {
            // TODO: Need to stuff a bunch of stuff into the command here like user name, ip, OS
            // whatever we can extract from the context that looks useful

            var commandHandlerFactory = Resolver.Container.Resolve<ICommandHandlerFactory>();
            var commandFactory = Resolver.Container.Resolve<ICommandFactory>();
            commandHandlerFactory.ExecuteCommand(commandFactory.Create(command));
        }
    }
}
