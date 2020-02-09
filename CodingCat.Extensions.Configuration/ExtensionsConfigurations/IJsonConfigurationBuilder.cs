using CodingCat.Extensions.Configuration.Enums;
using CodingCat.Extensions.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;
using IBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;
using IConfigurationSource = CodingCat.Extensions.Configuration.Interfaces.IConfigurationSource;

namespace CodingCat.Extensions.Configuration.ExtensionsConfigurations
{
    public static class IJsonConfigurationBuilder
    {
        public static IBuilder RegisterJson(
            this IBuilder builder,
            Type configurationType,
            string fullPathToFolder,
            Environment environment,
            bool isOptional
        )
        {
            return builder.Register(
                configurationType,
                fullPathToFolder,
                environment,
                FileType.Json,
                isOptional
            );
        }

        public static IBuilder RegisterJson(
            this IBuilder builder,
            Type configurationType,
            Environment environment,
            bool isOptional
        )
        {
            return builder.Register(
                configurationType,
                environment,
                FileType.Json,
                isOptional
            );
        }

        //public static IBuilder RegisterJson(
        //    this IBuilder builder,
        //    string path,
        //    Type configurationType,
        //    Environment environment,
        //    bool isOptional
        //)
        //{

        //}

        //public static IBuilder RegisterJson(
        //    this IBuilder builder,
        //    IConfigurationSource source
        //)
        //{
        //    var isDefault = source.Environment == Environment.Default;
        //    var path = source.DirectoryInfo?.FullName;
        //    var fileName = new StringBuilder()
        //        .Append(source.ConfigurationType.Name)
        //        .Append(isDefault ? "" : $".{source.Environment.ToString()}")
        //        .Append(".json")
        //        .ToString();
        //    return builder.AddJsonFile(fileName, source.IsOptional);
        //}
    }
}
