using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCat.Extensions.Configuration.ExtensionsConfigurations
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AutoConfigure<T>(
            this IServiceCollection services,
            IConfiguration configurations
        ) where T : class
        {
            //return services.Configure<T>()
            return services.Configure<T>(
                configurations.GetSection(typeof(T).Name)
            );
        }
    }
}
