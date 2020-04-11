using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdescatApi.Serveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace IdescatApi.Serveis.Tests
{
    [TestClass()]
    public class PopulationTests
    {
        [TestMethod()]
        public void SugTest()
        {
            var client = new IdescatApiClient();

            Assert.ThrowsException<ArgumentException>(() => client.Population.Sug(""), 
                "The string of the q filter must have at least 2 characters.");

            string[] entities = client.Population.Sug("sa");

            string[] entities2 = client.Population.Sug("va", TerritorialEntity.County);

            string[] entities3 = client.Population.Sug("sa", new List<TerritorialEntity>() { TerritorialEntity.Municipality });

            HttpResponseMessage manualResponse = client.Population.ManualAsync("sug.txt?q=sa&tipus=mun").GetAwaiter().GetResult();
            string[] entitiesManual = manualResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult().Split('\n');
            Assert.AreEqual(entities3.Length, entitiesManual.Length);
            for(int i = 0; i < entities3.Length; i++)
            {
                Assert.AreEqual(entities3[i], entitiesManual[i]);
            }

            // assert data
            string[] entities4 = client.Population.Sug("sabadell", TerritorialEntity.Municipality);
            Assert.AreEqual(1, entities4.Length);
            Assert.AreEqual("Sabadell", entities4[0]);
        }

        [TestMethod()]
        public void CercaTest()
        {
            var client = new IdescatApiClient();

            client.Population.Cerca("sabadell");
        }
    }
}