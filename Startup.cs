using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AgapeaNETCORE.Infraestructura.EndPoints;
using Microsoft.AspNetCore.Routing;
using AgapeaNETCORE.Infraestructura.RouteConstraint;
using AgapeaNETCORE.Models;
using AgapeaNETCORE.Models.Interfaces;

namespace AgapeaNETCORE
{
    public class Startup
    {

        #region "start up"
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //....configuración en el almacenamiento en cookie de estado de sesion -----
            services.AddSession((SessionOptions opc) =>
            {
                opc.Cookie.MaxAge = new TimeSpan(0, 30, 0); //cuando pase 30 min se borra la cookie
            });

            //inyecion de dependencias a controladores
            services.AddScoped<IDBAccess, SQLServerDBAAcces>(); ///Hay que añadir esto para que nos vaya la interfaz
            services.AddScoped<IClienteEnvioMail, ClienteMAILJet>(); //Para mandar emails por cada cliente creado

            //registramos restricciones sobre segmentos en patrones de enrutamiento----
            services.Configure<RouteOptions>(
                options => options.ConstraintMap.Add("tiposext", typeof(TiposExtensionRouteConstraint))
                );
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession(); //<----- middleware para crear la cookie con la variable de sesión y mandarla al cliente....

            app.UseAuthorization();
            //middleware que detecta que endpoint va a ejecutar el modulo de enrutamiendo
            //poner siempre tras el modulo oapp.UseRouting().......
            /*  app.Use(async (contexto, next) =>
               {
                   Endpoint end = contexto.GetEndpoint();
                   if (end != null)
                   {
                       await contexto
                           .Response
                           .WriteAsync($"el endpoint seleccionado es {end.DisplayName}");
                   }
                   else
                   {
                       await contexto
                           .Response
                           .WriteAsync($"ningun endpoint seleccionado,no se cumple ningun patron");

                   }

                   await contexto.Response.WriteAsync($"el endpoint seleccionado es:{end.DisplayName}");
               });*/
            //una funcion a la que metemos otra funcion(lambda) 
            //Si no pones el tipo no pasa nada,el programa se fia pero si haces algo mal casca

            app.UseEndpoints(endpoints =>
            {
                //Primer endpoint
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Registro",
                    pattern: "{controller=Cliente}/{action=Registro}/{id?}");


                //Segundo endpoint
                /* endpoints
                 .MapGet("/capitalPais/{pais}", Capitalpaises.EndPoint)
                 .WithDisplayName("endpoint ejecuta capitalpaises");*/

                //Tercer endpoint
                endpoints.MapGet(
                 "/files/{nombrefichero:alpha}.{extension:tiposext}",
             //:regex(^[a-zA-Z]{{3}}$)
             async context =>
             {
                 foreach (var nombresegmento in context.Request.RouteValues)
                 {
                     await context
                     .Response
                     .WriteAsync($"segmento:{nombresegmento.Key}.valor {nombresegmento.Value}\n");
                 }
             });


            });
        }













        /*
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
            //
             //------------------- configuración de inyeccion de dependencias o servicios ---------------------
             //   se especifica que objetos crear cuanod unas clases piden objetos que implementan determinadas
           //     interfaces      ej: si un controlador me pide un objeto IDBAcces ==> objeto SQLServerDBAcces
            //

            // ------- resgistramos restricciones sobre segmentos en patrones de enrutamiento ---------
            
             // ------ME DA UN FALLO Y NO SE PORQUE--------
            services.Configure<RouteOptions>(
                options => options.ConstraintMap.Add("tiposext", typeof(TiposExtensionRouteConstraint))
                );
                //----------------------------------------------------------------------------------------
            
            
            services.AddScoped<IDBAccess, SQLServerDBAccess>();
            
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
        #endregion*/

    }
}
