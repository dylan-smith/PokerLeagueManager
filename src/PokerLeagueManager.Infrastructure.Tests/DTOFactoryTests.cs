using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.DTO;
using System.Data;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class DTOFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDataRow()
        {
            var sut = new DTOFactory();

            sut.Create<GetGameCountByDateDTO>(null);
        }

        [TestMethod]
        public void SampleDTOFromDataRow()
        {
            var sut = new DTOFactory();
            var sampleTable = GenerateSampleDataTable();

            var gameId = Guid.NewGuid();
            var gameYear = 2010;
            var gameMonth = 7;
            var gameDay = 3;

            sampleTable.Rows.Add(gameId, gameYear, gameMonth, gameDay);

            var result = sut.Create<GetGameCountByDateDTO>(sampleTable.Rows[0]);

            Assert.IsNotNull(result);
            Assert.AreEqual(gameId, result.GameId);
            Assert.AreEqual(gameYear, result.GameYear);
            Assert.AreEqual(gameMonth, result.GameMonth);
            Assert.AreEqual(gameDay, result.GameDay);
        }

        private DataTable GenerateSampleDataTable()
        {
            var result = new DataTable();

            result.Columns.Add("GameId", typeof(Guid));
            result.Columns.Add("GameYear", typeof(int));
            result.Columns.Add("GameMonth", typeof(int));
            result.Columns.Add("GameDay", typeof(int));

            return result;
        }
    }
}
