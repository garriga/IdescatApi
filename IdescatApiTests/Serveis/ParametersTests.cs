using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace IdescatApi.Serveis.Tests
{
    [TestClass()]
    public class ParametersTests
    {
        [TestMethod()]
        public void AddTest()
        {
            var param = new Parameters();
            param.Add("a", "test1");
            Assert.IsTrue(param.Params.ContainsKey("a"));
            Assert.AreEqual("test1", param.Params["a"]);
        }

        [TestMethod()]
        public void AddTest1()
        {
            var param = new Parameters();
            param.Add("test", TerritorialEntity.Municipality);
            Assert.IsTrue(param.Params.ContainsKey("test"));
            Assert.AreEqual("mun", param.Params["test"]);
        }

        [TestMethod()]
        public void AddTest2()
        {
            var param = new Parameters();
            param.Add("test", new List<TerritorialEntity>() { 
                TerritorialEntity.Municipality, 
                TerritorialEntity.ATP,
                TerritorialEntity.Catalunya});
            Assert.IsTrue(param.Params.ContainsKey("test"));
            Assert.AreEqual("mun,atp,cat", param.Params["test"]);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var param = new Parameters();
            param.Add("test", new List<TerritorialEntity>() {
                TerritorialEntity.Municipality,
                TerritorialEntity.ATP,
                TerritorialEntity.Catalunya});
            param.Add("test1", TerritorialEntity.Municipality);
            param.Add("test2", "testing");
            Assert.AreEqual("test=mun,atp,cat&test1=mun&test2=testing", param.ToString());
        }
    }
}