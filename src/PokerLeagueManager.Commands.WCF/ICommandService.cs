using PokerLeagueManager.Common.Commands.Infrastructure;
using System.ServiceModel;

namespace PokerLeagueManager.Commands.WCF
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CommandTypeProvider))]
    public interface ICommandService
    {
        [OperationContract]
        void ExecuteCommand(ICommand command);
    }
}
