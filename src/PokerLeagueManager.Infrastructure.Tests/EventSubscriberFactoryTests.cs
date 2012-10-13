using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using System.Data;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class EventSubscriberFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWithNullRow()
        {
            var sut = new EventSubscriberFactory();

            sut.Create(null);
        }

        [TestMethod]
        public void CreateWithValidSubscriber()
        {
            var subscriberId = Guid.NewGuid();
            var subscriberUrl = "http://www.foo.com";

            var subscriberTable = GenerateSampleDataTable();
            var testRow = subscriberTable.Rows.Add(subscriberId, subscriberUrl);
            
            var sut = new EventSubscriberFactory();

            var result = sut.Create(testRow);

            Assert.IsNotNull(result);
            Assert.AreEqual(subscriberId, result.SubscriberId);
            Assert.AreEqual(subscriberUrl, result.SubscriberUrl);
        }

        private DataTable GenerateSampleDataTable()
        {
            var result = new DataTable();

            result.Columns.Add("SubscriberId", typeof(Guid));
            result.Columns.Add("SubscriberUrl", typeof(string));

            return result;
        }
    }
}
