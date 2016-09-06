using System;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public interface ICommandRepository
    {
        void LogCommand(ICommand command);

        void LogFailedCommand(ICommand command, Exception ex);
    }
}
