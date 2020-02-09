using System;
using System.IO;
using CodingCat.Extensions.Configuration.Enums;
using CodingCat.Extensions.Configuration.ExtensionsConfigurations;
using CodingCat.Extensions.Configuration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            var expected = new ConfigA("Config1", 3, true);

            // Act
            var actual = this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Build()
                .Bind<ConfigA>();

            // Assert
            expected.AssertWith(actual);
        }

        [TestMethod]
        public void Test_AutoBind_MultiConfig_Ok()
        {
            // Arrange
            var expectedA = new ConfigA("Config1", 3, true);
            var expectedB = new ConfigA("Config1", 2, true);

            // Act
            var builder = this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Auto<ConfigB>(FileType.Json)
                .Build();
            var actualA = builder.Bind<ConfigA>();
            var actualB = builder.Bind<ConfigB>();

            // Assert
            expectedA.AssertWith(actualA);
            expectedB.AssertWith(actualB.ConfigA);
        }

        [TestMethod]
        public void Test_ConfigureWithDI_Ok()
        {
            // Arrange
            var expected = new ConfigA("Config1", 3, true);
            var configurations = this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Build();

            var provider = new ServiceCollection()
                .AddOptions()
                .AutoConfigure<ConfigA>(configurations)
                .BuildServiceProvider();

            // Act
            var actual = provider
                .GetRequiredService<IOptions<ConfigA>>()
                .Value;

            // Assert
            expected.AssertWith(actual);
        }

        [TestMethod]
        public void Test_ConfigureWithDI_HotReload_Ok()
        {
            // Arrange
            PrepareHotReloadFile();

            var expected = new ConfigA("Config1", 4, true);
            var configurations = this.Builder
                .Auto<ConfigA>(FileType.Json)
                .Build();

            var provider = new ServiceCollection()
                .AddOptions()
                .AutoConfigure<ConfigA>(configurations)
                .BuildServiceProvider();

            // Act
            var old = provider.GetService<IOptions<ConfigA>>().Value;

            ModifyHotReloadFile(expected);
            var actual = provider
                .GetRequiredService<IOptionsSnapshot<ConfigA>>()
                .Value;

            // Assert
            Console.WriteLine(JsonConvert.SerializeObject(
                old, Formatting.Indented
            ));

            expected.AssertWith(actual);
        }

        public static FileInfo GetHotReloadFile()
        {
            return new FileInfo(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "App_Data/Configurations",
                "ConfigA.Staging.json"
            ));
        }

        public static FileInfo PrepareHotReloadFile()
        {
            var file = GetHotReloadFile();
            if (file.Exists) file.Delete();

            using (var writer = file.AppendText())
                writer.Write("{}");

            return file;
        }

        public static FileInfo ModifyHotReloadFile(ConfigA config)
        {
            var file = PrepareHotReloadFile();
            File.WriteAllText(
                file.FullName,
                JsonConvert.SerializeObject(
                    new
                    {
                        configA = config
                    }, Formatting.Indented
                )
            );

            System.Threading.Thread.Sleep(1000);
            return file;
        }
    }
}
