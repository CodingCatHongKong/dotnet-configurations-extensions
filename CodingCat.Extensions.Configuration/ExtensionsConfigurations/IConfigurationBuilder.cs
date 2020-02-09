using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using IBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;
using IConfigurationSource = CodingCat.Extensions.Configuration.Interfaces.IConfigurationSource;
using Environment = CodingCat.Extensions.Configuration.Enums.Environment;
using CodingCat.Extensions.Configuration.Enums;
using System.Text;

namespace CodingCat.Extensions.Configuration.ExtensionsConfigurations
{
    public static class IConfigurationBuilder
    {
        public static IBuilder SetRelativePath(
            this IBuilder builder,
            string relativePath
        )
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                relativePath
            );
            return builder.SetBasePath(path);
        }

        public static IBuilder Register(
            this IBuilder builder,
            Type configurationType,
            string fullPathToFolder,
            Environment environment,
            FileType fileType,
            bool isOptional
        )
        {
            var isDefault = environment == Environment.Default;
            var filename = new StringBuilder()
                .Append(configurationType.Name)
                .Append(isDefault ? "" : $".{environment.ToString()}")
                .Append(".json")
                .ToString();
            var path = string.IsNullOrEmpty(fullPathToFolder) ?
                filename :
                Path.Combine(fullPathToFolder, filename);

            switch (fileType)
            {
                case FileType.Json:
                    return builder.AddJsonFile(path, isOptional);
            }

            throw new NotSupportedException(fileType.ToString());
        }

        public static IBuilder Register(
            this IBuilder builder,
            Type configurationType,
            Environment environment,
            FileType fileType,
            bool isOptional
        )
        {
            return builder.Register(
                configurationType,
                null,
                environment,
                fileType,
                isOptional
            );
        }

        public static IBuilder Register(
            this IBuilder builder,
            IConfigurationSource source
        )
        {
            return builder.Register(
                source.ConfigurationType,
                source.ConfigurationsDirectory?.FullName,
                source.Environment,
                source.FileType,
                source.IsOptional
            );
        }

        public static IBuilder Register(
            this IBuilder builder,
            params IConfigurationSource[] sources
        )
        {
            foreach (var source in sources) builder.Register(source);
            return builder;
        }
    }
}
