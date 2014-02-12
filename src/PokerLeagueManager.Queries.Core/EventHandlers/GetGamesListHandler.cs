using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetGamesListHandler : BaseHandler, IHandlesEvent<GameCreatedEvent>, IHandlesEvent<PlayerAddedToGameEvent>
    {
        public void Handle(GameCreatedEvent e)
        {
            QueryDataStore.Insert(new GetGamesListDto()
            {
                GameId = e.AggregateId,
                GameDate = e.GameDate,
                NumPlayers = 0,
                Winner = string.Empty
            });
        }

        public void Handle(PlayerAddedToGameEvent e)
        {
            var game = QueryDataStore.GetData<GetGamesListDto>(x => x.GameId == e.AggregateId);

            game.NumPlayers++;
            
            if (e.Placing == 1)
            {
                game.Winner = e.PlayerName;
            }
            
            QueryDataStore.Update<GetGamesListDto>(game);
        }
    }
}
