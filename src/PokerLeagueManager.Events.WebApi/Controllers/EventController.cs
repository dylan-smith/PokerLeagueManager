using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokerLeagueManager.Common.Infrastructure;
using PokerLeagueManager.Queries.Core.Infrastructure;
using Unity;

namespace PokerLeagueManager.Events.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventController : ApiController, IEventService
    {
        public HttpResponseMessage Post(string eventName, [FromBody]JToken jsonbody)
        {
            var eventType = GetEventType(eventName);

            var e = (IEvent)Activator.CreateInstance(eventType);

            if (jsonbody != null)
            {
                e = (IEvent)JsonConvert.DeserializeObject(jsonbody.ToString(), eventType);
            }

            var ai = new TelemetryClient();
            ai.TrackEvent(eventType.Name, e.GetPropertiesDictionary());

            HandleEvent(e);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public void HandleEvent(IEvent e)
        {
            var eventHandlerFactory = Resolver.Container.Resolve<IEventHandlerFactory>();
            eventHandlerFactory.HandleEvent(e);
        }

        private Type GetEventType(string eventName)
        {
            List<Type> assemblyTypes = new List<Type>();

            assemblyTypes.AddRange(typeof(BaseEvent).Assembly.GetTypes());

            return assemblyTypes.Single(t => t.IsClass && t.Name == $"{eventName}Event");
        }
    }
}
