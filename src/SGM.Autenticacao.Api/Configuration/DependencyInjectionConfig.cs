using SGM.Autenticacao.Application.Services;
using SGM.Autenticacao.Domain.Interfaces.Repository;
using SGM.Autenticacao.Domain.Interfaces.Services;
using SGM.Autenticacao.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SGM.Autenticacao.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();

            services.AddScoped<IMunicipioService, MunicipioService>();
            services.AddScoped<IMunicipioRepository, MunicipioRepository>();

            services.AddTransient<UsuarioService>();

            services.AddScoped<IEmailService, EmailService>();
       
        }
    }
}
