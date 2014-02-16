using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Tests;
using PokerLeagueManager.Common.Utilities;
using PokerLeagueManager.Queries.Core;
using PokerLeagueManager.Queries.Core.Infrastructure;

namespace PokerLeagueManager.Commands.Tests.Infrastructure
{
    [TestClass]
    public abstract class BaseTestFixture
    {
        public virtual void Setup()
        {
        }

        public virtual IEnumerable<IEvent> Given()
        {
            return new List<IEvent>();
        }

        public virtual IEnumerable<IEvent> ExpectedEvents()
        {
            return new List<IEvent>();
        }

        public virtual Exception ExpectedException()
        {
            return null;
        }

        public void RunTest(ICommand command)
        {
            Setup();

            var repository = new FakeEventRepository();
            repository.InitialEvents = Given();

            var queryDataStore = new FakeQueryDataStore();
            var queryService = new QueryHandler(queryDataStore);

            HandleEvents(repository.InitialEvents, queryDataStore);
            
            Exception caughtException = null;
            var commandHandlerFactory = new CommandHandlerFactory(repository, queryService);

            try
            {
                commandHandlerFactory.ExecuteCommand(command);
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

            EventsAssert.AreEqual(ExpectedEvents().ToList<IEvent>(), repository.EventList);
        }

        protected Guid AnyGuid()
        {
            return EventsAssert.AnyGuid();
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
