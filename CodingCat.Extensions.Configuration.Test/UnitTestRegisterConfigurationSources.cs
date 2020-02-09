using System;
using CodingCat.Extensions.Configuration.ExtensionsConfigurations;
using CodingCat.Extensions.Configuration.Impls;
using CodingCat.Extensions.Configuration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;
using IConfigurationBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;

namespace CodingCat.Extensions.Configuration.Test
{
    [TestClass]
    public class UnitTestRegisterConfigurationSources
    {
        public IConfigurationBuilder Builder { get; private set; }

        [TestInitialize]
        public void Init()
        {
            this.Builder = new ConfigurationBuilder()
                .SetRelativePath("App_Data/Configurations");
        }

        //[TestMethod]
        //public void Test_AddFromSources_Ok()
        //{
        //    // Arrange
        //    var expectedConfig1 = "Config1";
        //    var expectedConfig2 = 2;
        //    var expectedConfig3 = true;

        //    // Act
        //    var actual = new ConfigA();
        //    this.Builder
        //        .RegisterJson(new ConfigurationSource<ConfigA>(
        //            Environment.Default,
        //            )
        //        .Build()
        //        .GetSection(nameof(ConfigA))
        //        .Bind(actual);

        //    // Assert
        //    Console.WriteLine(JsonConvert.SerializeObject(
        //        actual, Formatting.Indented
        //    ));

        //    Assert.IsNotNull(actual);
        //    Assert.AreEqual(expectedConfig1, actual.Config1);
        //    Assert.AreEqual(expectedConfig2, actual.Config2);
        //    Assert.AreEqual(expectedConfig3, actual.Config3);
        //}
    }
}
