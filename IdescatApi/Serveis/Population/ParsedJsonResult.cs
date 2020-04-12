using Newtonsoft.Json;
using QuickType;
using System.Collections.Generic;

namespace IdescatApi.Serveis
{
    public partial class ParsedJsonPopulation
    {
        [JsonProperty("feed")]
        public Feed Feed { get; set; }
        public static ParsedJsonPopulation FromJson(string json) => JsonConvert.DeserializeObject<ParsedJsonPopulation>(json, QuickType.Converter.Settings);

        public List<Entity> GetEntities()
        {
            var entities = new List<Entity>();
            foreach(var entry in Feed.Entry)
            {
                var entity = new Entity
                {
                    Name = entry.Title,
                    MalePopulation = (int)entry.CrossDataSet.CrossSection.CrossObs[0].ObsValue,
                    FemalePopulateion = (int)entry.CrossDataSet.CrossSection.CrossObs[1].ObsValue,
                    Population = (int)entry.CrossDataSet.CrossSection.CrossObs[2].ObsValue,
                    TimePeriod = entry.CrossDataSet.CrossSection.TimePeriod.DateTime
                };
                // TODO: parse the entity type
                //entity.Type = entry.Category[2].Term.ToLower().GetEnum<TerritorialEntity>();
                entities.Add(entity);
            }
            return entities;
        }
    }
}
