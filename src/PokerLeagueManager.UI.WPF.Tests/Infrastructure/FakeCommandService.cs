using PokerLeagueManager.Common.Commands.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.UI.WPF.Tests.Infrastructure
{
    public class FakeCommandService : ICommandService
    {
        public List<ICommand> ExecutedCommands = new List<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            ExecutedCommands.Add(command);
        }
    }
}
