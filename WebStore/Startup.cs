﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore
{ 
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services) //колекция сервисов
        {
            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//Обработка исключений
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();// файлы
            app.UseStaticFiles(); //Обслуживания статический вайлов

            app.UseRouting(); //Муштиризацыя

            app.UseAuthorization(); // авторизацыя

            //var greetings = "Hello Friends";
            //var greetings = Configuration["Greetings"];

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages(); Использование встроенных стилей
            //});
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
                endpoints.MapControllerRoute(// Создание контролера
                    "Employees",
                    "{controller=Employees}/{action=Сотрудники}/{id?}");
                //endpoints.MapControllerRoute(// Создание контролера
                //    "Employe",
                //    "{controller=Employe}/{action=Сотрудник}/{Id}&{LastName}&{FirstName}&{Patronymic}&{Age}&{AboutTheEmployee}");
            });
        }
    }
}
