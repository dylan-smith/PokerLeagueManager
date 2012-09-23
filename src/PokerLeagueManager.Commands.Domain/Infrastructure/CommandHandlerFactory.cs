using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        // TODO: Store the Command in a database somewhere for logging/auditing
        // TODO: implement some kind of error handling for the command

        private IEventRepository _eventRepository { get; set; }

        public CommandHandlerFactory(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void ExecuteCommand<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException("command", "Cannot execute a null Command.");
            }

            FindCommandHandler<T>().Execute(command);
        }

        public void ExecuteCommand(ICommand command)
        {
            var executeCommandMethod = from m in typeof(CommandHandlerFactory).GetMethods()
                                       where m.Name == "ExecuteCommand" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition
                                       select m;

            if (executeCommandMethod.Count() != 1)
            {
                throw new Exception("Unexpected Exception. Could not find the ExecuteCommand method via Reflection.");
            }

            MethodInfo generic = executeCommandMethod.First().MakeGenericMethod(command.GetType());
            generic.Invoke(this, new object[] { command });
        }

        private IHandlesCommand<T> FindCommandHandler<T>() where T: ICommand
        {
            var matchingTypes = typeof(IHandlesCommand<>).FindHandlers<T>(Assembly.GetExecutingAssembly());

            if (matchingTypes.Count() == 0)
            {
                throw new ArgumentException(string.Format("Could not find Command Handler for {0}", typeof(T).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Command Handler for {0}", typeof(T).Name));
            }

            var result = (IHandlesCommand<T>)UnityHelper.Container.Resolve(matchingTypes.First(), null);
            result.Repository = _eventRepository;

            return result;
        }
    }
}
