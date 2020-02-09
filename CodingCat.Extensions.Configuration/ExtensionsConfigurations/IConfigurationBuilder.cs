using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using IBuilder = Microsoft.Extensions.Configuration.IConfigurationBuilder;

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
    }
}
