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
        public void InsertSampleDto()
        {
            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDtoFactory>();

            var sut = new QueryDataStore(mockDTOFactory.Object);
            sut.DatabaseLayer = mockDatabaseLayer.Object;

            var testDto = new GetGameCountByDateDto() 
            {
                GameId = Guid.NewGuid(),
                GameYear = 2012,
                GameMonth = 10,
                GameDay = 5
            };

            sut.Insert(testDto);

            var expectedSql = "INSERT INTO GetGameCountByDate(GameId, GameYear, GameMonth, GameDay) VALUES(@GameId, @GameYear, @GameMonth, @GameDay)";

            mockDatabaseLayer.Verify(x => x.ExecuteNonQuery(expectedSql, "@GameId", testDto.GameId, "@GameYear", testDto.GameYear, "@GameMonth", testDto.GameMonth, "@GameDay", testDto.GameDay));
        }

        [TestMethod]
        public void GetSampleDtoWithNoResults()
        {
            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDtoFactory>();

            mockDatabaseLayer.Setup(x => x.GetDataTable(It.IsAny<string>())).Returns(GenerateSampleDataTable());

            var sut = new QueryDataStore(mockDTOFactory.Object);
            sut.DatabaseLayer = mockDatabaseLayer.Object;

            // the ToList() is necessary to force deferred execution to happen
            sut.GetData<GetGameCountByDateDto>().ToList();

            var expectedSql = "SELECT * FROM GetGameCountByDate";

            mockDatabaseLayer.Verify(x => x.GetDataTable(expectedSql));
        }

        [TestMethod]
        public void GetSampleDtoWithTwoResults()
        {
            var sampleTable = GenerateSampleDataTable();
            sampleTable.Rows.Add(Guid.NewGuid(), 2010, 7, 3);
            sampleTable.Rows.Add(Guid.NewGuid(), 2012, 11, 27);

            var mockDatabaseLayer = new Mock<IDatabaseLayer>();
            var mockDTOFactory = new Mock<IDtoFactory>();

            mockDatabaseLayer.Setup(x => x.GetDataTable(It.IsAny<string>())).Returns(sampleTable);

            var sut = new QueryDataStore(mockDTOFactory.Object);
            sut.DatabaseLayer = mockDatabaseLayer.Object;

            // the ToList() is necessary to force deferred execution to happen
            sut.GetData<GetGameCountByDateDto>().ToList();

            var expectedSql = "SELECT * FROM GetGameCountByDate";

            mockDatabaseLayer.Verify(x => x.GetDataTable(expectedSql));
            mockDTOFactory.Verify(x => x.Create<GetGameCountByDateDto>(sampleTable.Rows[0]));
            mockDTOFactory.Verify(x => x.Create<GetGameCountByDateDto>(sampleTable.Rows[1]));
        }

        private DataTable GenerateSampleDataTable()
        {
            var result = new DataTable();

            try
            {
                result.Columns.Add("GameId", typeof(Guid));
                result.Columns.Add("GameYear", typeof(int));
                result.Columns.Add("GameMonth", typeof(int));
                result.Columns.Add("GameDay", typeof(int));

                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
        }
    }
}
