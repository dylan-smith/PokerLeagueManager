using System.Collections.Generic;
using PokerLeagueManager.Common.Commands.Infrastructure;

namespace PokerLeagueManager.UI.WPF.Tests.Infrastructure
{
    public class FakeCommandService : ICommandService
    {
        public FakeCommandService()
        {
            ExecutedCommands = new List<ICommand>();
        }

        public List<ICommand> ExecutedCommands { get; private set; }

        public void ExecuteCommand(ICommand command)
        {
            ExecutedCommands.Add(command);
        }
    }
}
