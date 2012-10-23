using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PokerLeagueManager.Common.Events.Infrastructure;
using PokerLeagueManager.Common.Utilities;

namespace PokerLeagueManager.Queries.Core.Infrastructure
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
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

            MethodInfo generic = executeEventHandler.First().MakeGenericMethod(e.GetType());
            generic.Invoke(this, new object[] { e });
        }

        private IHandlesEvent<T> FindEventHandler<T>() where T : IEvent
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
