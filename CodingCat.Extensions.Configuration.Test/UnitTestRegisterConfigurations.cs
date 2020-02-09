using CodingCat.Extensions.Configuration.Enums;
using CodingCat.Extensions.Configuration.ExtensionsConfigurations;
using CodingCat.Extensions.Configuration.Impls;
using CodingCat.Extensions.Configuration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Test_BuildFromSource_ConfigAJson_Ok()
        {
            // Arrange
            var expected = new ConfigA("Config1", 3, true);

            var source = new ConfigurationSource<ConfigA>(
                Environment.Default,
                FileType.Json,
                false
            );

            // Act
            var actual = new ConfigA();
            this.Builder
                .Register(source)
                .Register(source.With(Environment.Develop))
                .Build()
                .GetSection(nameof(ConfigA))
                .Bind(actual);

            // Assert
            expected.AssertWith(actual);
        }

        [TestMethod]
        public void Test_BuildFromSources_ConfigAJson_Ok()
        {
            // Arrange
            var expected = new ConfigA("Config1", 3, true);

            var source = new ConfigurationSource<ConfigA>(
                Environment.Default,
                FileType.Json,
                false
            );

            // Act
            var actual = new ConfigA();
            this.Builder
                .Register(
                    source,
                    source.With(Environment.Develop)
                )
                .Build()
                .GetSection(nameof(ConfigA))
                .Bind(actual);

            // Assert
            expected.AssertWith(actual);
        }

        [TestMethod]
        public void Test_BuildFromOptionalSources_ConfigAJson_Ok()
        {
            // Arrange
            var expected = new ConfigA("Config1", 2, true);

            var source = new ConfigurationSource<ConfigA>(
                Environment.Default,
                FileType.Json,
                false
            );

            // Act
            var actual = new ConfigA();
            this.Builder
                .Register(
                    source,
                    source.With(Environment.Production).With(true)
                )
                .Build()
                .GetSection(nameof(ConfigA))
                .Bind(actual);

            // Assert
            expected.AssertWith(actual);
        }

        [TestMethod]
        public void Test_AutoBuild_ConfigAJson_Ok()
        {
            // Arrange
            var expected = new ConfigA("Config1", 3, true);

            // Act
            var actual = new ConfigA();
            this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Build()
                .GetSection(nameof(ConfigA))
                .Bind(actual);

            // Assert
            expected.AssertWith(actual);
        }
    }
}