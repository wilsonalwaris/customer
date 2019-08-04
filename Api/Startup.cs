using Api.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.dependencyRegister = new DependencyRegister();
        }

        public IConfiguration Configuration { get; }

        private readonly IDependencyRegister dependencyRegister;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            this.dependencyRegister.LoadServices(services);

            services.AddSwaggerGen(setupAction => {
                setupAction.SwaggerDoc(
                    "CustomerOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                         Title = "Customer Api",
                         Version = "1"
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/CustomerOpenApiSpecification/swagger.json",
                   "Customer Api");
                setupAction.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}
