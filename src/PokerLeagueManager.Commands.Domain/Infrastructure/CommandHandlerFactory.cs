using System;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IQueryService _queryService;
        private readonly ICommandRepository _commandRepository;

        public CommandHandlerFactory(IEventRepository eventRepository, IQueryService queryService, ICommandRepository commandRepository)
        {
            _eventRepository = eventRepository;
            _queryService = queryService;
            _commandRepository = commandRepository;
        }

        public void ExecuteCommand<T>(T command)
            where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Cannot execute a null Command.");
            }

            try
            {
                _commandRepository.LogCommand(command);
                FindCommandHandler<T>().Execute(command);
            }
            catch (Exception ex)
            {
                _commandRepository.LogFailedCommand(command, ex);
                throw;
            }
        }

        public void ExecuteCommand(ICommand command)
        {
            var executeCommandMethod = from m in typeof(CommandHandlerFactory).GetMethods()
                                       where m.Name == "ExecuteCommand" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition
                                       select m;

            if (executeCommandMethod.Count() != 1)
            {
                throw new InvalidOperationException("Unexpected Exception. Could not find the ExecuteCommand method via Reflection.");
            }

            MethodInfo generic = executeCommandMethod.First().MakeGenericMethod(command.GetType());

            try
            {
                generic.Invoke(this, new object[] { command });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private IHandlesCommand<T> FindCommandHandler<T>()
            where T : ICommand
        {
            var matchingTypes = typeof(IHandlesCommand<>).FindHandlers<T>(Assembly.GetExecutingAssembly());

            if (!matchingTypes.Any())
            {
                throw new ArgumentException(string.Format("Could not find Command Handler for {0}", typeof(T).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Command Handler for {0}", typeof(T).Name));
            }

            var result = (IHandlesCommand<T>)UnitySingleton.Container.Resolve(matchingTypes.First(), null);
            result.Repository = _eventRepository;
            result.QueryService = _queryService;

            return result;
        }
    }
}
