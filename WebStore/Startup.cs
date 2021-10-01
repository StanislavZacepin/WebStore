using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Indentity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services.InMemory;
using WebStore.Services.InSQL;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)  //колекция сервисов
        {
            services.AddDbContext<WebStoreDB>(opt => 
                 opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddIdentity<User, Role>(/*opt => {  opt.}*/)
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "Top.WebStore";
                opt.Cookie.HttpOnly = true;

                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;

            });

            services.AddTransient<WebStoreDbInitializer>();

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddScoped<IProductData, SqlProductData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddSingleton<IBlogsData, InMemoryBlogData>();
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

            app.UseStatusCodePagesWithRedirects("~/Home/Status{0}");

            app.UseStaticFiles();  //Обслуживания статический вайлов

            app.UseRouting();  //Муштиризацыя

            app.UseAuthentication(); //аутентификация
            app.UseAuthorization();  // авторизацыя

            app.UseMiddleware<TestMiddleware>();

            app.UseWelcomePage("/Welcome");

          
          

            
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
