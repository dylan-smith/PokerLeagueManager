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
        private IPrincipal _principal;
        private IGuidService _guidService;
        private IDateTimeService _dateTimeService;

        public CommandFactory(IPrincipal currentPrincipal, IGuidService guidService, IDateTimeService dateTimeService)
        {
            _principal = currentPrincipal;
            _guidService = guidService;
            _dateTimeService = dateTimeService;
        }

        public T Create<T>() where T : ICommand, new()
        {
            var result = new T();

            if (_principal == null || _principal.Identity == null || string.IsNullOrEmpty(_principal.Identity.Name))
            {
                result.User = "Unknown";
            }
            else
            {
                result.User = _principal.Identity.Name;
            }

            result.CommandId = _guidService.NewGuid();
            result.Timestamp = _dateTimeService.Now();
            result.IsAsynchronous = false;

            return result;
        }
    }
}
