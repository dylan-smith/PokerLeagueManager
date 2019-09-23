using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetPlayerCountByNameEventHandler : BaseEventHandler, IHandlesEvent<PlayerCreatedEvent>, IHandlesEvent<PlayerDeletedEvent>
    {
        public void Handle(PlayerCreatedEvent e)
        {
            QueryDataStore.Insert(new GetPlayerCountByNameDto()
            {
                PlayerId = e.PlayerId,
                PlayerName = e.PlayerName,
            });
        }

        public void Handle(PlayerDeletedEvent e)
        {
            var dto = QueryDataStore.GetData<GetPlayerCountByNameDto>().Single(d => d.PlayerId == e.PlayerId);
            QueryDataStore.Delete(dto);
        }
    }
}
