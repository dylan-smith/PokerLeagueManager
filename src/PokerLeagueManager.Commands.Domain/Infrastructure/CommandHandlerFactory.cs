using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private IEventRepository _eventRepository;
        private IQueryService _queryService;

        public CommandHandlerFactory(IEventRepository eventRepository, IQueryService queryService)
        {
            _eventRepository = eventRepository;
            _queryService = queryService;
        }

        public void ExecuteCommand<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException("command", "Cannot execute a null Command.");
            }

            FindCommandHandler<T>().Execute(command);
        }

        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "This Exception should never happen, so I'm ok with leaving it as-is")]
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

        private IHandlesCommand<T> FindCommandHandler<T>() where T : ICommand
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
            result.QueryService = _queryService;

            return result;
        }
    }
}
