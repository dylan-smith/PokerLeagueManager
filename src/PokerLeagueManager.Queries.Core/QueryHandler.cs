using System;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core
{
    public class QueryHandler : IQueryService
    {
        private IQueryDataStore _queryDataStore;

        public QueryHandler(IQueryDataStore queryDataStore)
        {
            _queryDataStore = queryDataStore;
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            return _queryDataStore.GetData<GetGameCountByDateDTO>().Count(x => x.GameYear == gameDate.Year &&
                                                                               x.GameMonth == gameDate.Month &&
                                                                               x.GameDay == gameDate.Day);
        }
    }
}
