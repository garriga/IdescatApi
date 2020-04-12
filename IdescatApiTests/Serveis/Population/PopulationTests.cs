using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdescatApi.Serveis.Tests
{
    [TestClass()]
    public class PopulationTests
    {
        [TestMethod()]
        public void SugAsyncTest()
        {
            var client = new IdescatApiClient();

            Assert.ThrowsException<ArgumentException>(() => client.Population.SugAsync("").GetAwaiter().GetResult(), 
                "The string of the q filter must have at least 2 characters.");

            string[] entities = client.Population.SugAsync("sa").GetAwaiter().GetResult();

            string[] entities2 = client.Population.SugAsync("va", TerritorialEntity.County).GetAwaiter().GetResult();

            string[] entities3 = client.Population.SugAsync("sa", 
                new List<TerritorialEntity>() { TerritorialEntity.Municipality }).GetAwaiter().GetResult();

            HttpResponseMessage manualResponse = client.Population.ManualAsync("sug.txt?q=sa&tipus=mun").GetAwaiter().GetResult();
            string[] entitiesManual = manualResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult().Split('\n');
            Assert.AreEqual(entities3.Length, entitiesManual.Length);
            for(int i = 0; i < entities3.Length; i++)
            {
                Assert.AreEqual(entities3[i], entitiesManual[i]);
            }

            // assert data
            string[] entities4 = client.Population.SugAsync("sabadell", TerritorialEntity.Municipality)
                .GetAwaiter().GetResult();
            Assert.AreEqual(1, entities4.Length);
            Assert.AreEqual("Sabadell", entities4[0]);
        }

        [TestMethod()]
        public async Task CercaAsyncTest()
        {
            var client = new IdescatApiClient();

            var parsed = await client.Population.CercaAsync("sabadell");
            var entities = parsed.GetEntities();
            Assert.AreEqual(4, entities.Count);

            var parsed2 = await client.Population.CercaAsync("sabadell", TerritorialEntity.Municipality);
            var entities2 = parsed2.GetEntities();
            Assert.AreEqual(1, entities2.Count);            
            Entity sabadell = entities[0];
            Assert.AreEqual("Sabadell", sabadell.Name);
        }
    }
}