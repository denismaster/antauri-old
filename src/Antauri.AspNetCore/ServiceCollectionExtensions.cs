using System;
using Antauri.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Antauri.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntauri(this IServiceCollection services, Action<AntauriOptionsBuilder> optionsBuilder = null)
        {
            services.AddSingleton<BlockChain>();
            services.AddSingleton<IHashProvider, SHA256HashProvider>();
            return services;
        }
    }
}
