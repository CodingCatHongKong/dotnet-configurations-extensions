using Microsoft.Extensions.Configuration;
using System;
using IConfig = Microsoft.Extensions.Configuration.IConfiguration;

namespace CodingCat.Extensions.Configuration.ExtensionsConfigurations
{
    public static class IConfiguration
    {
        public static object Bind(this IConfig config, Type type)
        {
            var instance = Activator.CreateInstance(type);
            config.Bind(type.Name, instance);
            return instance;
        }

        public static T Bind<T>(this IConfig config)
            where T : class, new() => (T)config.Bind(typeof(T));
    }
}
