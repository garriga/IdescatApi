using IdescatApi.Serveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdescatApi
{
    public class IdescatApiClient
    {
        private readonly Uri BaseUri = new Uri("https://api.idescat.cat");
        private readonly HttpClient _httpClient;

        // list of services
        public readonly Population Population;

        public IdescatApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = BaseUri
            };
            Population = new Population(_httpClient);
        }

        public Task<HttpResponseMessage> ManualAsync(string requestString)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestString);
            return _httpClient.SendAsync(request);
        }

    }
}
