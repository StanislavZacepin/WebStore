using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Indentity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.Services.Data;
using WebStore.Services.Services.InCookies;
using WebStore.Services.Services.InMemory;
using WebStore.Services.Services.InSQL;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Values;

namespace WebStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)  //колекция сервисов
        {
            var database_type = Configuration["Database"];

            switch (database_type)
            {
                default: throw new InvalidOperationException($"Тип БД {database_type} не подlерживается");

                case "SqlServer":
                    services.AddDbContext<WebStoreDB>(opt =>
                 opt.UseSqlServer(Configuration.GetConnectionString(database_type)));
                   break;

                case "Sqlite":
                    services.AddDbContext<WebStoreDB>(opt =>
                 opt.UseSqlite(Configuration.GetConnectionString(database_type),
                 o => o.MigrationsAssembly("WebStore.DAL.Sqlite")));
                    break;

                case "InMemory":
                    services.AddDbContext<WebStoreDB>(opt => opt.UseInMemoryDatabase("Webstore.db"));
                    break;

            }

            
            

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
            services.AddScoped<ICartService,  InCookiesCartService>();
            services.AddScoped<IOrderService,  SqlOrderService>();


            #region Http Сервисы
            services.AddHttpClient<IValuesService, ValuesClient>(
                    client => client.BaseAddress = new(Configuration["WebAPI"]));

            services.AddHttpClient<IEmployeesData, EmployeesClient>(
                client => client.BaseAddress = new(Configuration["WebAPI"])); 
            #endregion

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

               
                    endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                   

                    endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
                
               
            });
        }
    }
}
