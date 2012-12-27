using System;
using System.ServiceModel;

namespace PokerLeagueManager.Common.DTO
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        int GetGameCountByDate(DateTime gameDate);
    }
}
