using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.ApplicationInsights;
using PokerLeagueManager.Common.Infrastructure;

namespace PokerLeagueManager.Commands.Domain.Infrastructure
{
    public class EventServiceProxy : IEventService, IEventServiceProxy, IDisposable
    {
        private HttpClient _eventClient;
        private bool _disposedValue = false;
        private string _serviceUrl;

        public EventServiceProxy()
        {
            _eventClient = new HttpClient();
            _eventClient.DefaultRequestHeaders.Accept.Clear();
            _eventClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string ServiceUrl
        {
            get
            {
                return _serviceUrl;
            }

            set
            {
                _serviceUrl = value;
                _eventClient.BaseAddress = new Uri(_serviceUrl);
            }
        }

        public void HandleEvent(IEvent e)
        {
            var actionName = GetActionName(e);

            var aiData = new Dictionary<string, string>();
            aiData.Add("EventName", e.GetType().ToString());
            aiData.Add("Subscriber", ServiceUrl);
            aiData.Add("EventData", Newtonsoft.Json.JsonConvert.SerializeObject(e, Newtonsoft.Json.Formatting.Indented));

            var ai = new TelemetryClient();
            ai.TrackEvent("EventPublished", aiData);

            var task = _eventClient.PostAsJsonAsync($"/{actionName}", e);
            task.Wait();
            var response = task.Result;
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _eventClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        private object GetActionName(IEvent e)
        {
            var eventName = e.GetType().Name;
            return eventName.Substring(0, eventName.Length - "Event".Length);
        }
    }
}
