using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests
{
    [TestClass]
    public class ZeroGamesTest : BaseQueryTest
    {
        [TestMethod]
        public void GetGamesList_ZeroGames()
        {
            var query = new GetGamesListQuery();
            RunTest<IEnumerable<GetGamesListDto>>(query);
        }

        public override IEnumerable<IDataTransferObject> ExpectedDtos()
        {
            return new List<IDataTransferObject>();
        }
    }
}
