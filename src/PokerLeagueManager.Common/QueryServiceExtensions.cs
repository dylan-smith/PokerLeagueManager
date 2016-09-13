using System;
using System.Collections.Generic;
using System.Linq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;

namespace PokerLeagueManager.Common
{
    public static class QueryServiceExtensions
    {
        public static int GetGameCountByDate(this IQueryService queryService, DateTime gameDate)
        {
            var query = new GetGameCountByDateQuery() { GameDate = gameDate };
            return queryService.ExecuteQueryInt(query);
        }

        public static IEnumerable<GetGamesListDto> GetGamesList(this IQueryService queryService)
        {
            var query = new GetGamesListQuery();
            return queryService.ExecuteQueryList(query).Cast<GetGamesListDto>();
        }

        public static IEnumerable<GetGamePlayersDto> GetGamePlayers(this IQueryService queryService, Guid gameId)
        {
            var query = new GetGamePlayersQuery() { GameId = gameId };
            return queryService.ExecuteQueryList(query).Cast<GetGamePlayersDto>();
        }

        public static IEnumerable<GetPlayerStatisticsDto> GetPlayerStatistics(this IQueryService queryService)
        {
            var query = new GetPlayerStatisticsQuery();
            return queryService.ExecuteQueryList(query).Cast<GetPlayerStatisticsDto>();
        }

        public static IEnumerable<GetPlayerGamesDto> GetPlayerGames(this IQueryService queryService, string playerName)
        {
            var query = new GetPlayerGamesQuery() { PlayerName = playerName };
            return queryService.ExecuteQueryList(query).Cast<GetPlayerGamesDto>();
        }

        public static IEnumerable<GetGamesWithPlayerDto> GetGamesWithPlayer(this IQueryService queryService, string playerName)
        {
            var query = new GetGamesWithPlayerQuery() { PlayerName = playerName };
            return queryService.ExecuteQueryList(query).Cast<GetGamesWithPlayerDto>();
        }

        public static GetPlayerByNameDto GetPlayerByName(this IQueryService queryService, string playerName)
        {
            var query = new GetPlayerByNameQuery() { PlayerName = playerName };
            return (GetPlayerByNameDto)queryService.ExecuteQueryDto(query);
        }
    }
}
