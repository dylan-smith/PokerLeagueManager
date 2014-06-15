
using System.Collections.Generic;
using PokerLeagueManager.Common.DTO;
namespace EFSpike.Domain
{
    public class Repository
    {
        public IEnumerable<GetGameResultsDto> GetResults()
        {
            var ctx = new DtoContext();

            return ctx.GetResults();
        }
    }
}
