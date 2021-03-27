using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace SGM.Autenticacao.Api.Configuration
{
    public static class CORSConfig
    {
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsPolicyBuilder corsBuilder = InstantiateCorsPolicyBuilder(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("ApiCorsPolicy", corsBuilder.Build());
            });
        }

        private static CorsPolicyBuilder InstantiateCorsPolicyBuilder(IConfiguration configuration)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins(GetAllOrigins(configuration));
            corsBuilder.AllowCredentials();
            return corsBuilder;
        }

        private static string[] GetAllOrigins(IConfiguration configuration)
        {
            var origins = new List<string>();

            configuration.GetSection("CorsOrigins").AsEnumerable().ToList().ForEach(item =>
            {
                if (!string.IsNullOrEmpty(item.Value))
                    origins.Add(item.Value);
            });

            return origins.ToArray();
        }
    }
}
