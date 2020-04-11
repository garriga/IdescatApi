using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdescatApi.Serveis
{
    class RequestMaker
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage request;

        public Task<HttpResponseMessage> GetAsync()
        {
            return _httpClient.SendAsync(request);
        }

        public HttpResponseMessage Get()
        {
            return GetAsync().GetAwaiter().GetResult();
        }
    }
}
