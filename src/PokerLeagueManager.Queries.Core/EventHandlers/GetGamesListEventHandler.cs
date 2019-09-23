using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGamesListEventHandler : BaseEventHandler, IHandlesEvent<GameCreatedEvent>, IHandlesEvent<GameDeletedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GetGamesListDto()
            {
                GameId = e.GameId,
                GameDate = e.GameDate,
            });
        }

        public void Handle(GameDeletedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamesListDto>().Single(d => d.GameId == e.GameId);
            QueryDataStore.Delete(dto);
        }
    }
}
