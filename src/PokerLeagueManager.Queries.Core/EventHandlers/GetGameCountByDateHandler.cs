using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGameCountByDateHandler : BaseEventHandler, IHandlesEvent<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            DatabaseLayer.ExecuteNonQuery(
                "INSERT INTO GetGameCountByDateHandler(GameId, GameYear, GameMonth, GameDay) VALUES(@GameId, @GameYear, @GameMonth, @GameDay)",
                "@GameId", e.AggregateId,
                "@GameYear", e.GameDate.Year,
                "@GameMonth", e.GameDate.Month,
                "@GameDay", e.GameDate.Day);
        }
    }
}
