using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGamesListEventHandler : BaseEventHandler, IHandlesEvent<GameCreatedEvent>, IHandlesEvent<GameDeletedEvent>, IHandlesEvent<GameCompletedEvent>, IHandlesEvent<GameUncompletedEvent>
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

        public void Handle(GameCompletedEvent e)
        {
            var winnerId = e.Placings.Single(p => p.Value == 1).Key;
            var winner = QueryDataStore.GetData<PlayerLookupDto>().Single(p => p.PlayerId == winnerId);
            var game = QueryDataStore.GetData<GetGamesListDto>().Single(g => g.GameId == e.GameId);

            var dto = new GetGamesListDto()
            {
                DtoId = game.DtoId,
                GameId = e.GameId,
                GameDate = game.GameDate,
                Winner = winner.PlayerName,
                Winnings = e.First,
                Completed = true,
            };

            QueryDataStore.Update(dto);
        }

        public void Handle(GameUncompletedEvent e)
        {
            var dto = QueryDataStore.GetData<GetGamesListDto>().Single();

            dto.Completed = false;

            QueryDataStore.Update(dto);
        }
    }
}
