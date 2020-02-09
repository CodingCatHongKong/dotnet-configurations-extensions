using System;
using CodingCat.Extensions.Configuration.Enums;
using CodingCat.Extensions.Configuration.ExtensionsConfigurations;
using CodingCat.Extensions.Configuration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IConfigurationBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;

namespace CodingCat.Extensions.Configuration.Test
{
    [TestClass]
    public class UnitTestAutoBind
    {
        public IConfigurationBuilder Builder { get; private set; }

        [TestInitialize]
        public void Init()
        {
            this.Builder = new ConfigurationBuilder()
                .SetRelativePath("App_Data/Configurations");
        }

        [TestMethod]
        public void Test_ConfigA_AutoBound()
        {
            // Arrange
            var expected = UnitTestRegisterConfigurations
                .GetExpected("Config1", 3, true);

            // Act
            var actual = this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Build()
                .Bind<ConfigA>();

            // Assert
            UnitTestRegisterConfigurations
                .AssertConfigA(expected, actual);
        }
    }
}
