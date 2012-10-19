using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Commands.Domain.Infrastructure;

namespace PokerLeagueManager.Infrastructure.Tests
{
    [TestClass]
    public class EventSubscriberFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWithNullRow()
        {
            var sut = new EventSubscriberFactory(null);

            sut.Create(null);
        }

        [TestMethod]
        public void CreateWithValidSubscriber()
        {
            var subscriberId = Guid.NewGuid();
            var subscriberUrl = "http://www.foo.com";

            var subscriberTable = GenerateSampleDataTable();
            var testRow = subscriberTable.Rows.Add(subscriberId, subscriberUrl);

            var mockEventServiceProxyFactory = new Mock<IEventServiceProxyFactory>();

            var sut = new EventSubscriberFactory(mockEventServiceProxyFactory.Object);

            var result = sut.Create(testRow);

            Assert.IsNotNull(result);
            Assert.AreEqual(subscriberId, result.SubscriberId);
            Assert.AreEqual(subscriberUrl, result.SubscriberUrl);

            mockEventServiceProxyFactory.Verify(x => x.Create(subscriberUrl));
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
