using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFExceptionHandling.Common;

namespace WCFExceptionHandling.Server
{
    public class CommandService : ICommandService
    {
        public int ExecuteCommand()
        {
            throw new GameWithNotEnoughPlayersException();
            //return 42;
        }
    }
}
