using System.Linq;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GameLookupEventHandler : BaseEventHandler,
                                          IHandlesEvent<GameCreatedEvent>,
                                          IHandlesEvent<GameDateChangedEvent>,
                                          IHandlesEvent<GameCompletedEvent>,
                                          IHandlesEvent<GameUncompletedEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GameLookupDto()
            {
                GameId = e.GameId,
                GameDate = e.GameDate,
                Completed = false,
            });
        }

        public void Handle(GameDateChangedEvent e)
        {
            var dto = QueryDataStore.GetData<GameLookupDto>().Single(x => x.GameId == e.GameId);
            dto.GameDate = e.GameDate;
            QueryDataStore.Update(dto);
        }

        public void Handle(GameCompletedEvent e)
        {
            var dto = QueryDataStore.GetData<GameLookupDto>().Single(x => x.GameId == e.GameId);
            dto.Completed = true;
            QueryDataStore.Update(dto);
        }

        public void Handle(GameUncompletedEvent e)
        {
            var dto = QueryDataStore.GetData<GameLookupDto>().Single(x => x.GameId == e.GameId);
            dto.Completed = false;
            QueryDataStore.Update(dto);
        }
    }
}
