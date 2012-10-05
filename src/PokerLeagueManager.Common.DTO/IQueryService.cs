using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.DTO
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        int GetGameCountByDate(DateTime gameDate);
    }
}
