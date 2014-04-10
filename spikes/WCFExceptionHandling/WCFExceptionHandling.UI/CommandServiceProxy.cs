using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFExceptionHandling.Common;

namespace WCFExceptionHandling.UI
{
    public class CommandServiceProxy : ClientBase<ICommandService>, ICommandService
    {
        public int ExecuteCommand()
        {
            return base.Channel.ExecuteCommand();
        }
    }
}
