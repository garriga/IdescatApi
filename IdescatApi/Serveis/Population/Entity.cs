using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdescatApi.Serveis
{
    public class Entity
    {
        public TerritorialEntity Type { get; set; }
        public string Name { get; set; }
        public int FemalePopulateion { get; set; }
        public int MalePopulation { get; set; }
        public int Population { get; set; }
        public DateTime TimePeriod { get; set;}
    }   
}
