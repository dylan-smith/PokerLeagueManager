using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

namespace PokerLeagueManager.Common.Infrastructure
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(QueryTypeProvider))]
    public interface IQueryService
    {
        [OperationContract]
        IDataTransferObject ExecuteQueryDto(IQuery query);

        [OperationContract]
        IEnumerable<IDataTransferObject> ExecuteQueryList(IQuery query);

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", Justification = "Couldnt think of a better name")]
        [OperationContract]
        int ExecuteQueryInt(IQuery query);

        TResult Execute<TResult>(IQuery<TResult> query);
    }
}