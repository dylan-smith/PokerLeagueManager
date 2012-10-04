using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PokerLeagueManager.Queries.WCF
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        int GetGameCountByDate(DateTime gameDate);
    }
}
