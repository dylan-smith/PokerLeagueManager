using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Lookups;
using PokerLeagueManager.Common.Events;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Core.EventHandlers
{
    public class GetPlayerStatisticsHandler : BaseHandler, IHandlesEvent<PlayerAddedToGameEvent>, IHandlesEvent<GameDeletedEvent>, IHandlesEvent<PlayerRenamedEvent>
    {
        public void Handle(PlayerAddedToGameEvent e)
        {
            var player = QueryDataStore.GetData<GetPlayerStatisticsDto>().FirstOrDefault(x => x.PlayerName.ToUpper() == e.PlayerName.ToUpper());

            if (player == null)
            {
                player = new GetPlayerStatisticsDto();

                AddGameToPlayer(player, e);

                QueryDataStore.Insert<GetPlayerStatisticsDto>(player);
            }
            else
            {
                AddGameToPlayer(player, e);

                QueryDataStore.SaveChanges();
            }
        }

        public void Handle(GameDeletedEvent e)
        {
            var players = QueryDataStore.GetData<LookupGamePlayersDto>().Where(x => x.GameId == e.GameId);

            foreach (var p in players)
            {
                var stats = QueryDataStore.GetData<GetPlayerStatisticsDto>().Single(x => x.PlayerName.ToUpper() == p.PlayerName.ToUpper());

                stats.GamesPlayed--;
                stats.Winnings -= p.Winnings;
                stats.PayIn -= p.PayIn;
                stats.Profit -= p.Winnings - p.PayIn;
                stats.ProfitPerGame = stats.GamesPlayed == 0 ? 0 : (double)stats.Profit / stats.GamesPlayed;

                if (stats.GamesPlayed == 0)
                {
                    QueryDataStore.Delete(stats);
                }
            }

            QueryDataStore.SaveChanges();
        }

        public void Handle(PlayerRenamedEvent e)
        {
            var oldPlayer = QueryDataStore.GetData<GetPlayerStatisticsDto>().Single(x => x.PlayerName.ToUpper() == e.OldPlayerName.ToUpper());
            var mergePlayer = QueryDataStore.GetData<GetPlayerStatisticsDto>().FirstOrDefault(x => x.PlayerName.ToUpper() == e.NewPlayerName.ToUpper());

            if (mergePlayer == null)
            {
                oldPlayer.PlayerName = e.NewPlayerName;
                QueryDataStore.SaveChanges();
            }
            else
            {
                mergePlayer.GamesPlayed += oldPlayer.GamesPlayed;
                mergePlayer.Winnings += oldPlayer.Winnings;
                mergePlayer.PayIn += oldPlayer.PayIn;
                mergePlayer.Profit += oldPlayer.Profit;
                mergePlayer.ProfitPerGame = mergePlayer.GamesPlayed == 0 ? 0 : (double)mergePlayer.Profit / mergePlayer.GamesPlayed;

                QueryDataStore.Delete(oldPlayer);
            }
        }

        private void AddGameToPlayer(GetPlayerStatisticsDto player, PlayerAddedToGameEvent e)
        {
            // TODO: change this to use the elvis operator - can't do until I upgrade the build
            if (player.PlayerName == null || player.PlayerName.ToUpper() != e.PlayerName.ToUpper())
            {
                player.PlayerName = e.PlayerName;
            }

            player.GamesPlayed++;
            player.Winnings += e.Winnings;
            player.PayIn += e.PayIn;
            player.Profit += e.Winnings - e.PayIn;
            player.ProfitPerGame = (double)player.Profit / player.GamesPlayed;
        }
    }
}
