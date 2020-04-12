using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using QuickType;

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

        public async Task<ParsedJsonPopulation> CercaAsync(string Text)
        {
            var all = new List<TerritorialEntity>() {
                TerritorialEntity.Catalunya,
                TerritorialEntity.Province,
                TerritorialEntity.ATP,
                TerritorialEntity.County,
                TerritorialEntity.Municipality,
                TerritorialEntity.ColectiveEntities,
                TerritorialEntity.SingularEntities,
                TerritorialEntity.PopulationNucleus,
                TerritorialEntity.Scattered
            };
            return await CercaAsync(Text, all, new List<Similarity>(), Selector.All, Orderby.Type, 0); 
            //return await CercaAsync(Text, new List<TerritorialEntity>(), new List<Similarity>(), Selector.All, Orderby.Type, 0); 
        }
        public async Task<ParsedJsonPopulation> CercaAsync(string Text, TerritorialEntity territorialEntity)
        {
            return await CercaAsync(Text, new List<TerritorialEntity>() { territorialEntity }, new List<Similarity>(), Selector.All, Orderby.Type, 0);
        }
        public async Task<ParsedJsonPopulation> CercaAsync(string Text, List<TerritorialEntity> TerritorialEntities, List<Similarity> Similarities, 
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
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                //buscar entry i posar un [] si fa falta
                int startPos = content.IndexOf("entry");
                startPos += 7;
                // the json is not well made and changes depending if there is only one entry or several
                // so, we fix it by changing to json to the same format in case there is only one entry
                if (content[startPos] != '[')
                {
                    // we add [ and ] at the beginning and at the end of the entry section
                    content = content.Insert(startPos, "[");
                    // search end position
                    int endPos = content.IndexOf("\"xmlns\"") - 1;
                    content = content.Insert(endPos, "]");
                }
                return ParsedJsonPopulation.FromJson(content);
            }
            else
            {
                throw new HttpRequestException($"The request was responded with an HTTP Code: {response.StatusCode}");
            }
        }

        public async Task<string[]> SugAsync(string Start)
        {
            return await SugAsync(Start, new List<TerritorialEntity>());
        }
        public async Task<string[]> SugAsync(string Start, TerritorialEntity territorialEntity)
        {
            return await SugAsync(Start, new List<TerritorialEntity>() { territorialEntity });
        }
        public async Task<string[]> SugAsync(string Start, List<TerritorialEntity> TerritorialEntities)
        {
            if (Start.Length < 2)
            {
                throw new ArgumentException("The string of the q filter must have at least 2 characters.");
            }
            var param = new Parameters();
            param.Add("q", Start);
            param.Add("tipus", TerritorialEntities);
            var request = new HttpRequestMessage(HttpMethod.Get, Identifier + "sug.txt" + "?" + param.ToString());
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) 
            { 
                var content = await response.Content.ReadAsStringAsync();
                return content.Split('\n');
            } 
            else
            {
                throw new HttpRequestException($"The request was responded with an HTTP Code: {response.StatusCode}");
            }
        }

        public async Task<HttpResponseMessage> ManualAsync(string param)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Identifier + param);
            return await _httpClient.SendAsync(request);
        }
    }
}
