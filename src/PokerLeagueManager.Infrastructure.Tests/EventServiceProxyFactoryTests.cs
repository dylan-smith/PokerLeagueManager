using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Infrastructure;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class EventServiceProxyFactoryTests
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dont care in tests")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDataRow()
        {
            var sut = new EventServiceProxyFactory(new EventServiceProxy());

            sut.Create(null);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dont care in tests")]
        [TestMethod]
        public void CreateWithValidDataRow()
        {
            var sut = new EventServiceProxyFactory(new EventServiceProxy());
            var sampleTable = GenerateSampleDataTable();

            var subscriberId = Guid.NewGuid();
            var subscriberUrl = "http://www.pokerleaguemanager.com/";

            sampleTable.Rows.Add(subscriberId, subscriberUrl);

            var result = sut.Create(sampleTable.Rows[0]);

            Assert.IsNotNull(result);
            Assert.AreEqual(subscriberUrl, result.ServiceUrl);
        }

        private DataTable GenerateSampleDataTable()
        {
            var result = new DataTable();

            try
            {
                result.Columns.Add("SubscriberId", typeof(Guid));
                result.Columns.Add("SubscriberUrl", typeof(string));

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
