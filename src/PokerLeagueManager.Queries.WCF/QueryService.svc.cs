using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Queries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        public int GetGameCountByDate(DateTime gameDate)
        {
            throw new NotImplementedException();
        }
    }
}
