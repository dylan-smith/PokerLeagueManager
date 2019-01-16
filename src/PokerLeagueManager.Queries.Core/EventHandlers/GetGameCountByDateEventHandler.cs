using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGameCountByDateEventHandler : BaseEventHandler, IHandlesEvent<GameCreatedEvent>, IHandlesEvent<GameDeletedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GetGameCountByDateDto()
            {
                GameId = e.GameId,
                GameYear = e.GameDate.Year,
                GameMonth = e.GameDate.Month,
                GameDay = e.GameDate.Day,
            });
        }

        public void Handle(GameDeletedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGameCountByDateDto>().Single(d => d.GameId == e.GameId);
            QueryDataStore.Delete<GetGameCountByDateDto>(dto);
        }
    }
}
