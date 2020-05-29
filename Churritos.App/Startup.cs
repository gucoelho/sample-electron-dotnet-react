using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Churritos.Dominio.Dados;
using Churritos.Dominio.Repositorios;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Churritos.App
{
    public class Startup
    {
        
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ContextoDaAplicação>(options => 
                options.UseSqlite("Data Source=churritos.db;"));
            
            services.AddScoped<CoberturaRepositorio>();
            services.AddScoped<RecheioRepositorio>();
            services.AddScoped<CategoriaRepositorio>();
            services.AddScoped<ProdutoRepositório>();
            
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "client-app/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ContextoDaAplicação contexto)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            contexto.Database.Migrate();
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "client-app");
                
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
            
            if (HybridSupport.IsElectronActive)
            {
                ElectronBootstrap();
            }
        }


        private async void ElectronBootstrap()
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 1152,
                Height = 864,
                Show = false
            });
            
            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle("Administração Churritos");
        }
    }
}