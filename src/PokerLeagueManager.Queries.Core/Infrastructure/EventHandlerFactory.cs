using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class EventHandlerFactory
    {
        // TODO: Check for duplicate events to provide idempotency
        private IQueryDataStore _queryDataStore;

        public EventHandlerFactory(IQueryDataStore queryDataStore)
        {
            _queryDataStore = queryDataStore;
        }

        public void HandleEvent<T>(T e) where T : IEvent
        {
            if (e == null)
            {
                throw new ArgumentNullException("e", "Cannot handle a null event.");
            }

            FindEventHandler<T>().Handle(e);
        }

        public void HandleEvent(IEvent e)
        {
            var executeEventHandler = from m in typeof(EventHandlerFactory).GetMethods()
                                       where m.Name == "HandleEvent" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition
                                       select m;

            if (executeEventHandler.Count() != 1)
            {
                throw new Exception("Unexpected Exception. Could not find the HandleEvent method via Reflection.");
            }

            MethodInfo generic = executeEventHandler.First().MakeGenericMethod(e.GetType());
            generic.Invoke(this, new object[] { e });
        }

        private IHandlesEvent<T> FindEventHandler<T>() where T: IEvent
        {
            var matchingTypes = typeof(IHandlesEvent<>).FindHandlers<T>(Assembly.GetExecutingAssembly());

            if (matchingTypes.Count() == 0)
            {
                throw new ArgumentException(string.Format("Could not find Event Handler for {0}", typeof(T).Name));
            }

            if (matchingTypes.Count() > 1)
            {
                throw new ArgumentException(string.Format("Found more than 1 Event Handler for {0}", typeof(T).Name));
            }

            var result = (IHandlesEvent<T>)UnityHelper.Container.Resolve(matchingTypes.First(), null);
            result.QueryDataStore = _queryDataStore;

            return result;
        }
    }
}
