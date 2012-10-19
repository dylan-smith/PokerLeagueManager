using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class QueryDataStoreTests
    {
        [TestMethod]
        public void InsertSampleDTO()
        {
            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDTOFactory>();

            var sut = new QueryDataStore(mockDatabaseLayer.Object, mockDTOFactory.Object);

            var testDto = new GetGameCountByDateDTO() 
            {
                GameId = Guid.NewGuid(),
                GameYear = 2012,
                GameMonth = 10,
                GameDay = 5
            };

            sut.Insert(testDto);

            var expectedSql = string.Format(
                "INSERT INTO GetGameCountByDate(GameId, GameYear, GameMonth, GameDay) VALUES('{0}', {1}, {2}, {3})",
                testDto.GameId,
                testDto.GameYear,
                testDto.GameMonth,
                testDto.GameDay);

            mockDatabaseLayer.Verify(x => x.ExecuteNonQuery(expectedSql));
        }

        [TestMethod]
        public void GetSampleDTOWithNoResults()
        {
            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDTOFactory>();

            mockDatabaseLayer.Setup(x => x.GetDataTable(It.IsAny<string>())).Returns(GenerateSampleDataTable());

            var sut = new QueryDataStore(mockDatabaseLayer.Object, mockDTOFactory.Object);

            // the ToList() is necessary to force deferred execution to happen
            var result = sut.GetData<GetGameCountByDateDTO>().ToList();

            var expectedSql = "SELECT * FROM GetGameCountByDate";

            mockDatabaseLayer.Verify(x => x.GetDataTable(expectedSql));
        }

        [TestMethod]
        public void GetSampleDTOWithTwoResults()
        {
            var sampleTable = GenerateSampleDataTable();
            sampleTable.Rows.Add(Guid.NewGuid(), 2010, 7, 3);
            sampleTable.Rows.Add(Guid.NewGuid(), 2012, 11, 27);

            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDTOFactory>();

            mockDatabaseLayer.Setup(x => x.GetDataTable(It.IsAny<string>())).Returns(sampleTable);

            var sut = new QueryDataStore(mockDatabaseLayer.Object, mockDTOFactory.Object);

            // the ToList() is necessary to force deferred execution to happen
            var result = sut.GetData<GetGameCountByDateDTO>().ToList();

            var expectedSql = "SELECT * FROM GetGameCountByDate";

            mockDatabaseLayer.Verify(x => x.GetDataTable(expectedSql));
            mockDTOFactory.Verify(x => x.Create<GetGameCountByDateDTO>(sampleTable.Rows[0]));
            mockDTOFactory.Verify(x => x.Create<GetGameCountByDateDTO>(sampleTable.Rows[1]));
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
