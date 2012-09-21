using PokerLeagueManager.Common.Commands.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PokerLeagueManager.Commands.WCF
{
    public class CommandService : ICommandService
    {
        public void ExecuteCommand(ICommand command)
        {
        }
    }
}
