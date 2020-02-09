using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace CodingCat.Extensions.Configuration.Test.Configurations
{
    [TestClass]
    public class ConfigA
    {
        public string Config1 { get; set; }
        public int Config2 { get; set; }
        public bool Config3 { get; set; }

        #region Constructor(s)
        public ConfigA() { }

        public ConfigA(string config1, int config2, bool config3)
        {
            this.Config1 = config1;
            this.Config2 = config2;
            this.Config3 = config3;
        }
        #endregion

        public void AssertWith(ConfigA actual)
        {
            Console.WriteLine(JsonConvert.SerializeObject(
                actual, Formatting.Indented
            ));

            Assert.IsNotNull(actual);
            Assert.AreEqual(this.Config1, actual.Config1);
            Assert.AreEqual(this.Config2, actual.Config2);
            Assert.AreEqual(this.Config3, actual.Config3);
        }
    }
}
