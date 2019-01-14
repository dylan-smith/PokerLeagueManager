using System;
using System.Web;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class CommandFactory : ICommandFactory
    {
        private readonly HttpContextWrapper _currentContext;
        private readonly IGuidService _guidService;
        private readonly IDateTimeService _dateTimeService;

        public CommandFactory(HttpContextWrapper currentContext, IGuidService guidService, IDateTimeService dateTimeService)
        {
            _currentContext = currentContext;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
        }

        public T Create<T>()
            where T : ICommand, new()
        {
            var result = new T();

            return Create(result);
        }

        public T Create<T>(T cmd)
            where T : ICommand
        {
            cmd.IPAddress = _currentContext.Request.UserHostAddress;

            if (cmd.CommandId == Guid.Empty)
            {
                cmd.CommandId = _guidService.NewGuid();
            }

            cmd.Timestamp = _dateTimeService.Now();

            return cmd;
        }
    }
}
