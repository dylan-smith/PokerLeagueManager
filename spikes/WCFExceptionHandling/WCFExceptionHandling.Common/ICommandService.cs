using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFExceptionHandling.Common
{
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        int ExecuteCommand();
    }
}
