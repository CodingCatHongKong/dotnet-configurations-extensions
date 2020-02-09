using CodingCat.Extensions.Configuration.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;
using IBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;

namespace CodingCat.Extensions.Configuration.ExtensionsConfigurations
{
    public static class IJsonConfigurationBuilder
    {
        public static IBuilder RegisterJson(
            this IBuilder builder,
            Type configurationType,
            Environment environment,
            bool isOptional
        )
        {
            var isDefault = environment == Environment.Default;
            var fileName = new StringBuilder()
                .Append(configurationType.Name)
                .Append(isDefault ? "" : $".{environment.ToString()}")
                .Append(".json")
                .ToString();
            return builder.AddJsonFile(fileName, isOptional);
        }
    }
}
