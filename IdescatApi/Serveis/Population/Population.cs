using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdescatApi.Serveis
{
    public class Population
    {
        private readonly string Identifier = "pob/v1/";
        private readonly HttpClient _httpClient;

        public Population(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Cerca(string Text)
        {
            Cerca(Text, new List<TerritorialEntity>(), new List<Similarity>(), Selector.All, Orderby.Type, 0); 
        }

        public void Cerca(string Text, TerritorialEntity territorialEntity)
        {
            Cerca(Text, new List<TerritorialEntity>() { territorialEntity }, new List<Similarity>(), Selector.All, Orderby.Type, 0);

        }

        public void Cerca(string Text, List<TerritorialEntity> TerritorialEntities, List<Similarity> Similarities, 
            Selector selector, Orderby orderby, int position)
        {
            var param = new Parameters();
            param.Add("q", Text);
            param.Add("tipus", TerritorialEntities);
            param.Add("sim", Similarities);
            param.Add("selec", selector);
            param.Add("orderby", orderby);
            param.Add("posicio", position.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, Identifier + "cerca.json" + "?" + param.ToString());
            // TODO: fix the async things
            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //return content.Split('\n');
            }
            else
            {
                throw new HttpRequestException($"The request was responded with an HTTP Code: {response.StatusCode}");
            }
        }

        public string[] Sug(string Start)
        {
            return Sug(Start, new List<TerritorialEntity>());
        }
        public string[] Sug(string Start, TerritorialEntity territorialEntity)
        {
            return Sug(Start, new List<TerritorialEntity>() { territorialEntity });
        }
        public string[] Sug(string Start, List<TerritorialEntity> TerritorialEntities)
        {
            if (Start.Length < 2)
            {
                throw new ArgumentException("The string of the q filter must have at least 2 characters.");
            }
            var param = new Parameters();
            param.Add("q", Start);
            param.Add("tipus", TerritorialEntities);
            var request = new HttpRequestMessage(HttpMethod.Get, Identifier + "sug.txt" + "?" + param.ToString());
            // TODO: fix the async things
            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode) 
            { 
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return content.Split('\n');
            } 
            else
            {
                throw new HttpRequestException($"The request was responded with an HTTP Code: {response.StatusCode}");
            }
        }

        public Task<HttpResponseMessage> ManualAsync(string param)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Identifier + param);
            return _httpClient.SendAsync(request);
        }
    }
}
