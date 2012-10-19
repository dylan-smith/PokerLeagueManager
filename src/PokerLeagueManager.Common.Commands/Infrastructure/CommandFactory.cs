using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Commands.Infrastructure
{
    public class CommandFactory : ICommandFactory
    {
        private IIdentity _identity;
        private IGuidService _guidService;
        private IDateTimeService _dateTimeService;

        public CommandFactory(IIdentity currentIdentity, IGuidService guidService, IDateTimeService dateTimeService)
        {
            _identity = currentIdentity;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
        }

        public T Create<T>() where T : ICommand, new()
        {
            var result = new T();

            return Create(result);
        }

        public T Create<T>(T cmd) where T : ICommand
        {
            if (_identity == null || string.IsNullOrEmpty(_identity.Name))
            {
                cmd.User = "Unknown";
            }
            else
            {
                cmd.User = _identity.Name;
            }

            cmd.CommandId = _guidService.NewGuid();
            cmd.Timestamp = _dateTimeService.Now();
            cmd.IsAsynchronous = false;

            return cmd;
        }
    }
}
