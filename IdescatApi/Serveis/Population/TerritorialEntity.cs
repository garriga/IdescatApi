using System.ComponentModel;

namespace IdescatApi.Serveis
{
    public enum TerritorialEntity
    {
        [Description("cat")]
        Catalunya,

        [Description("prov")]
        Province,

        [Description("atp")]
        ATP,

        [Description("com")]
        County,

        [Description("mun")]
        Municipality,

        [Description("ec")]
        ColectiveEntities,

        [Description("es")]
        SingularEntities,

        [Description("np")]
        PopulationNucleus,
        //probably no longer used
        [Description("dis")]
        Scattered
    }
}
