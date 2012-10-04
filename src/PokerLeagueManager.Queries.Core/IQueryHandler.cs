using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerLeagueManager.Queries.Core
{
    public interface IQueryHandler
    {
        int GetGameCountByDate(DateTime gameDate);
    }
}
