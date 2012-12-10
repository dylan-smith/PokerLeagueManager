using System;
using System.Collections.Generic;
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
        private IDatabaseLayer _databaseLayer;

        public EventHandlerFactory(IQueryDataStore queryDataStore, IDatabaseLayer databaseLayer)
        {
            _queryDataStore = queryDataStore;
            _databaseLayer = databaseLayer;
        }

        public void HandleEvent<T>(T e) where T : IEvent
        {
            if (e == null)
            {
                throw new ArgumentNullException("e", "Cannot handle a null event.");
            }

            if (!IdempotencyCheck(e))
            {
                return;
            }

            foreach (var handler in FindEventHandlers<T>())
            {
                handler.Handle(e);
            }
        }

        public void HandleEvent(IEvent e)
        {
            var executeEventHandler = from m in typeof(EventHandlerFactory).GetMethods()
                                      where m.Name == "HandleEvent" && m.ContainsGenericParameters && m.IsGenericMethod && m.IsGenericMethodDefinition
                                      select m;

            MethodInfo generic = executeEventHandler.First().MakeGenericMethod(e.GetType());
            generic.Invoke(this, new object[] { e });
        }

        private bool IdempotencyCheck<T>(T e) where T : IEvent
        {
            var eventCount = (int)_databaseLayer.ExecuteScalar("SELECT COUNT(*) FROM EventsProcessed WHERE EventId = @EventId", "@EventId", e.EventId);

            return eventCount == 0;
        }

        private IEnumerable<IHandlesEvent<T>> FindEventHandlers<T>() where T : IEvent
        {
            var matchingTypes = typeof(IHandlesEvent<>).FindHandlers<T>(Assembly.GetExecutingAssembly());

            foreach (var handler in matchingTypes)
            {
                var result = (IHandlesEvent<T>)UnityHelper.Container.Resolve(handler, null);
                result.QueryDataStore = _queryDataStore;

                yield return result;
            }
        }
    }
}
