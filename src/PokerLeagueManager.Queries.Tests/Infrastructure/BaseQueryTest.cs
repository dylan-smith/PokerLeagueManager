using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Common.DTO;
using PokerLeagueManager.Common.DTO.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Tests;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.Queries.Core;
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

        public IQueryService SetupQueryService()
        {
            var queryDataStore = new FakeQueryDataStore();

            HandleEvents(Given(), queryDataStore);

            return new QueryHandler(queryDataStore);
        }

        public void RunTest(Func<IQueryService, IEnumerable<IDataTransferObject>> query)
        {
            var queryDataStore = new FakeQueryDataStore();

            HandleEvents(Given(), queryDataStore);

            var queryService = new QueryHandler(queryDataStore);
            
            Exception caughtException = null;
            IEnumerable<IDataTransferObject> results = null;

            try
            {
                results = query(queryService);
            }
            catch (Exception e)
            {
                if (ExpectedException() == null)
                {
                    throw;
                }

                caughtException = e.InnerException;
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

            ListComparer.AreEqual(ExpectedDtos(), results, false);
        }

        protected Guid AnyGuid()
        {
            return ListComparer.AnyGuid();
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
