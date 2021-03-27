using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SGM.Autenticacao.Api.Configuration;
using SGM.Autenticacao.Application.Mapper;

namespace SGM.Autenticacao.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext(Configuration);
            services.ConfigToken(Configuration);
            services.AddCors(Configuration);
            services.AddDependencyInjection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers();   
            services.AddSwaggerGenConfig();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())            
                app.UseDeveloperExceptionPage();

            app.UseSwaggerConfig();
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new CustomExceptionMiddleware().Invoke
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
