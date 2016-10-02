using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PokerLeagueManager.Common.Infrastructure
{
    public class QueryServiceProxy : IQueryService, IDisposable
    {
        private HttpClient _queryClient;
        private bool _disposedValue = false;

        public QueryServiceProxy()
        {
            var queryUrl = ConfigurationManager.AppSettings["QueryServiceUrl"];

            _queryClient = new HttpClient();
            _queryClient.BaseAddress = new Uri(queryUrl);
            _queryClient.DefaultRequestHeaders.Accept.Clear();
            _queryClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var actionName = GetActionName(query);
            var task = _queryClient.PostAsJsonAsync($"/{actionName}", query);
            task.Wait();
            var response = task.Result;

            if (response.IsSuccessStatusCode)
            {
                var contentTask = response.Content.ReadAsAsync<TResult>();
                contentTask.Wait();
                return contentTask.Result;
            }

            throw new InvalidOperationException($"{response.StatusCode}: {response.ReasonPhrase} - Failed to execute query");
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
                    _queryClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        private object GetActionName(IQuery query)
        {
            var queryName = query.GetType().Name;
            return queryName.Substring(0, queryName.Length - "Query".Length);
        }
    }
}
