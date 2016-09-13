using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGamePlayersEventHandler : BaseEventHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<GameDeletedEvent>, IHandlesEvent<PlayerRenamedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var dto = new GetGamePlayersDto()
            {
                GameId = e.GameId,
                PlayerName = e.PlayerName,
                Placing = e.Placing,
                Winnings = e.Winnings,
                PayIn = e.PayIn
            };

            QueryDataStore.Insert(dto);
        }

        public void Handle(GameDeletedEvent e)
        {
            var dtos = QueryDataStore.GetData<GetGamePlayersDto>().Where(x => x.GameId == e.GameId);

            while (dtos.Count() > 0)
            {
                QueryDataStore.Delete(dtos.First());
            }
        }

        public void Handle(PlayerRenamedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamePlayersDto>().Single(x => x.GameId == e.GameId && x.PlayerName == e.OldPlayerName);

            dto.PlayerName = e.NewPlayerName;

            QueryDataStore.SaveChanges();
        }
    }
}
