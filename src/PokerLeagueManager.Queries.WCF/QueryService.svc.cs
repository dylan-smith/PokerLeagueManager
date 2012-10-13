using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Queries.Core;
using PokerLeagueManager.Queries.WCF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Practices.Unity;

namespace PokerLeagueManager.Queries.WCF
{
    public class QueryService : IQueryService
    {
        private IQueryService _queryHandler;

        public QueryService()
        {
            _queryHandler = Resolver.Container.Resolve<IQueryService>();
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            return _queryHandler.GetGameCountByDate(gameDate);
        }
    }
}
