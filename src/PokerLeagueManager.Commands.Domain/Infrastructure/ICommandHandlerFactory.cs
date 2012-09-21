using PokerLeagueManager.Common.Commands.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface ICommandHandlerFactory
    {
        void ExecuteCommand<T>(T command) where T : ICommand;
        void ExecuteCommand(ICommand command);
    }
}
