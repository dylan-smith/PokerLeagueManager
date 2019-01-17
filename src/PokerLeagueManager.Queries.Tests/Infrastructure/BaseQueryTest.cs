using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Common.Tests;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Queries.Tests.Infrastructure
{
    [TestClass]
    public abstract class BaseQueryTest
    {
        public virtual IEnumerable<IEvent> Given()
        {
            return new List<IEvent>();
        }

        public virtual Exception ExpectedException()
        {
            return null;
        }

        public virtual IEnumerable<IDataTransferObject> ExpectedDtos()
        {
            return null;
        }

        public virtual IDataTransferObject ExpectedDto()
        {
            return null;
        }

        public IQueryService SetupQueryService()
        {
            var queryDataStore = new FakeQueryDataStore();

            HandleEvents(Given(), queryDataStore);

            return new QueryHandlerFactory(queryDataStore);
        }

        public void RunTest<T>(IQuery<T> query)
        {
            var queryService = SetupQueryService();

            Exception caughtException = null;

            T result = default(T);

            try
            {
                result = queryService.Execute(query);
            }
            catch (Exception e)
            {
                if (ExpectedException() == null)
                {
                    throw;
                }

                caughtException = e;
            }

            if (caughtException != null || ExpectedException() != null)
            {
                if (caughtException != null && ExpectedException() != null)
                {
                    Assert.AreEqual(ExpectedException().GetType(), caughtException.GetType());
                }
                else
                {
                    Assert.Fail("There was an Expected Exception but none was thrown.");
                }
            }
            else
            {
                if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
                {
                    ObjectComparer.AreEqual(ExpectedDtos(), (IEnumerable<object>)result, false);
                }
                else
                {
                    ObjectComparer.AreEqual(ExpectedDto(), result);
                }
            }
        }

        protected Guid AnyGuid()
        {
            return ObjectComparer.AnyGuid();
        }

        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int", Justification = "This is a type specific method")]
        protected int AnyInt()
        {
            return ObjectComparer.AnyInt();
        }

        private void HandleEvents(IEnumerable<IEvent> events, IQueryDataStore queryDataStore)
        {
            var mockIdempotencyChecker = new Mock<IIdempotencyChecker>();
            var mockDatabaseLayer = new Mock<IDatabaseLayer>();

            mockDatabaseLayer.Setup(x => x.ExecuteInTransaction(It.IsAny<Action>())).Callback<Action>(x => x());

            var eventHandler = new EventHandlerFactory(queryDataStore, mockIdempotencyChecker.Object, mockDatabaseLayer.Object);

            foreach (IEvent e in events)
            {
                eventHandler.HandleEvent(e);
            }
        }
    }
}
