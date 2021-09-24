using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{ 
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)  //колекция сервисов
        {
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            services.AddRazorPages();
            services.AddControllersWithViews
                (opt => opt.Conventions.Add(new TestControllerConvention())).AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //Обработка исключений
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

           // app.UseStatusCodePages();

            app.UseStaticFiles();  //Обслуживания статический вайлов

            app.UseRouting();  //Муштиризацыя

            app.UseAuthorization();  // авторизацыя

            app.UseMiddleware<TestMiddleware>();

            app.UseWelcomePage("/Welcome");

           // app.UseStatusCodePagesWithReExecute("/Error404/Index");

          

            
            app.UseEndpoints(endpoints => // маршруты конечный точек
            {
                #region Обращения к конфигурацыи Greetings Выключен
                //endpoints.MapGet("/greetings", async context =>
                //      {
                //    //await context.Response.WriteAsync(greetings);
                //    await context.Response.WriteAsync(Configuration["Greetings"]);
                //      }); 
                #endregion

                //endpoints.MapDefaultControllerRoute(); конфигурацыя маршрута Тоже что и 
                //endpoints.MapControllerRoute(
                //   "default",
                //   "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(// Создание контролера
                //    "Employees",
                //    "{controller=Employees}/{action=Сотрудники}/{id?}");
               
            });
        }
    }
}
