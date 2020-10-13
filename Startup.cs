using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AgapeaNETCORE
{
    public class Startup
    {

        #region "..........propiedades de clase............."
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region "..........metodos de clase.............."
        public IConfiguration Configuration { get; }

        #endregion

        #region "..........metodos PUBLICOS de la clase......."
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             ------------------- configuración de inyeccion de dependencias o servicios ---------------------
                se especifica que objetos crear cuanod unas clases piden objetos que implementan determinadas
                interfaces      ej: si un controlador me pide un objeto IDBAcces ==> objeto SQLServerDBAcces
            */

            // ------- resgistramos restricciones sobre segmentos en patrones de enrutamiento ---------
            /*
             * ------ME DA UN FALLO Y NO SE PORQUE--------
            services.Configure<RouteOptions>(
                options => options.ConstraintMap.Add("tiposext", typeof(TiposExtensionRouteConstraint))
                );
                //----------------------------------------------------------------------------------------
            */
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //....... definición de modulos de la pipeline ...................
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }
        #endregion

    }
}
