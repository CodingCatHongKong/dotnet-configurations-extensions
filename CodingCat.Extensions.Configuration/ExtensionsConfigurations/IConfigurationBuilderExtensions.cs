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
    public static class IConfigurationBuilderExtensions
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
                    return builder.AddJsonFile(path, isOptional, true);
                case FileType.Xml:
                    return builder.AddXmlFile(path, isOptional, true);
                case FileType.Ini:
                    return builder.AddIniFile(path, isOptional, true);
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

        public static IBuilder Auto(
            this IBuilder builder,
            Type configurationType,
            string fullPathToFolder,
            FileType fileType
        )
        {
            foreach (Environment environment in Enum.GetValues(
                typeof(Environment)
            ))
            {
                builder.Register(
                    configurationType,
                    fullPathToFolder,
                    environment,
                    fileType,
                    environment != Environment.Default
                );
            }
            return builder;
        }

        public static IBuilder Auto(
            this IBuilder builder,
            Type configurationType,
            FileType fileType
        ) => builder.Auto(configurationType, null, fileType);

        public static IBuilder Auto<T>(
            this IBuilder builder,
            string fullPathToFolder,
            FileType fileType
        ) => builder.Auto(typeof(T), fullPathToFolder, fileType);

        public static IBuilder Auto<T>(
            this IBuilder builder,
            FileType fileType
        ) => builder.Auto(typeof(T), null, fileType);
    }
}
