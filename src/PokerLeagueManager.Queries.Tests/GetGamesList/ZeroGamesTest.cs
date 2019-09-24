using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.Queries;
using PokerLeagueManager.Queries.Tests.Infrastructure;

namespace PokerLeagueManager.Queries.Tests.GetGamesList
{
    [TestClass]
    public class ZeroGamesTest : BaseQueryTest
    {
        [TestMethod]
        public void GetGamesList_TwoGames()
        {
            var result = SetupQueryService().Execute(new GetGamesListQuery());

            Assert.AreEqual(0, result.Count());
        }
    }
}
