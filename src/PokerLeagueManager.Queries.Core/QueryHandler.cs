using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core
{
    public class QueryHandler : IQueryHandler
    {
        private IDatabaseLayer _databaseLayer;

        public QueryHandler(IDatabaseLayer databaseLayer)
        {
            _databaseLayer = databaseLayer;
        }

        public int GetGameCountByDate(DateTime gameDate)
        {
            return (int)_databaseLayer.ExecuteScalar(
                string.Format(
                    "SELECT COUNT(*) FROM GetGameCountByDate WHERE GameYear = {0} AND GameMonth = {1} AND GameDay = {2}", 
                    gameDate.Year, 
                    gameDate.Month, 
                    gameDate.Day));
        }
    }
}
