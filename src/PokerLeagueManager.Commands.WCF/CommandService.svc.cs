﻿using PokerLeagueManager.Common.Commands.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Practices.Unity;
using PokerLeagueManager.Commands.Domain.Infrastructure;

namespace PokerLeagueManager.Commands.WCF
{
    public class CommandService : ICommandService
    {
        public void ExecuteCommand(ICommand command)
        {
            var commandHandlerFactory = Resolver.Container.Resolve<ICommandHandlerFactory>();

            commandHandlerFactory.ExecuteCommand(command);
        }
    }
}
