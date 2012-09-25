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

            if (_identity == null || string.IsNullOrEmpty(_identity.Name))
            {
                result.User = "Unknown";
            }
            else
            {
                result.User = _identity.Name;
            }

            result.CommandId = _guidService.NewGuid();
            result.Timestamp = _dateTimeService.Now();
            result.IsAsynchronous = false;

            return result;
        }
    }
}
