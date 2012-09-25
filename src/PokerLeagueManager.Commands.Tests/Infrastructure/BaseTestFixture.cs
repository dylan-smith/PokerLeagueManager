﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLeagueManager.Commands.Domain.Infrastructure;
using PokerLeagueManager.Common.Commands.Infrastructure;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Exception caughtException = null;
            var commandHandlerFactory = new CommandHandlerFactory(repository);

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
    }
}
