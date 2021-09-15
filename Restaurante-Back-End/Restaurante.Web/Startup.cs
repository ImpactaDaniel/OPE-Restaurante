using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurante.Application;
using Restaurante.Domain;
using Restaurante.Infra;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Restaurante.Web
{
    public class Startup
    {
        private readonly static string CORS_NAME = "Default";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddInfra(Configuration)
                .AddDomain()
                .AddApplication(Configuration)
                .AddLogging(configure => configure.AddFile("Logs/Restaurante-{Date}.txt"))
                .AddSwaggerGen(c => c
                                    .SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurante Service", Version = "v1" }))
                .AddCors(cors => cors
                                    .AddPolicy(CORS_NAME, policy => policy
                                                                    .AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                    .AllowAnyHeader()))
                .AddControllersWithViews();

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante Service V1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app                
                .UseCors(CORS_NAME)
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                .Initialize();
        }
    }
}
