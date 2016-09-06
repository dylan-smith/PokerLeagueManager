using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(CommandTypeProvider))]
    public interface ICommandService
    {
        [OperationContract]
        void ExecuteCommand(ICommand command);
    }
}
