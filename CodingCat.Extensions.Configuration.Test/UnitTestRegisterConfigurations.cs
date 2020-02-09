using System;
using CodingCat.Extensions.Configuration.ExtensionsConfigurations;
using CodingCat.Extensions.Configuration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;
using IConfigurationBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;

namespace CodingCat.Extensions.Configuration.Test
{
    [TestClass]
    public class UnitTestRegisterConfigurations
    {
        public IConfigurationBuilder Builder { get; private set; }

        [TestInitialize]
        public void Init()
        {
            this.Builder = new ConfigurationBuilder()
                .SetRelativePath("App_Data/Configurations");
        }

        [TestMethod]
        public void Test_BuildRegistered_ConfigAJson_Ok()
        {
            // Arrange
            string buildConfigKey(string key) => $"configA:{key}";

            var config1Key = buildConfigKey(nameof(ConfigA.Config1));
            var config2Key = buildConfigKey(nameof(ConfigA.Config2));
            var config3Key = buildConfigKey(nameof(ConfigA.Config3));

            var expectedConfig1 = "Config1";
            var expectedConfig2 = 2;
            var expectedConfig3 = true;

            // Act
            var actual = new ConfigA();
            this.Builder
                .RegisterJson(
                    typeof(ConfigA),
                    Environment.Default,
                    false
                )
                .Build()
                .GetSection(nameof(ConfigA))
                .Bind(actual);

            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(
                actual, Formatting.Indented
            ));

            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedConfig1, actual.Config1);
            Assert.AreEqual(expectedConfig2, actual.Config2);
            Assert.AreEqual(expectedConfig3, actual.Config3);
        }
    }
}
