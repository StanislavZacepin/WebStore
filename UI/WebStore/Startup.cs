using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.Domain.Entities.Indentity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.TestAPI;
using WebStore.Services.Services.InCookies;
using WebStore.Services.Services.InMemory;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Identity;
using WebStore.WebAPI.Clients.Orders;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Values;
using Microsoft.Extensions.Logging;
using WebStore.Logger;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;
using WebStore.Services.Services;

namespace WebStore
{
    public record Startup(IConfiguration Configuration)
    {
       
        public void ConfigureServices(IServiceCollection services)  //колекция сервисов
        {           
                services.AddIdentity<User, Role>()
                .AddIdentityWebStoreWebAPIClients()
                .AddDefaultTokenProviders();

            //services.AddIdentityWebStoreWebAPIClients();
            //services.AddHttpClient("WebStoreWebAPIIdentity", client => client.BaseAddress = new (Configuration["WebAPI"]))
            //    .AddTypedClient<IUserStore<User>, UsersClient>()
            //   .AddTypedClient<IUserRoleStore<User>, UsersClient>()
            //   .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
            //   .AddTypedClient<IUserEmailStore<User>, UsersClient>()
            //   .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
            //   .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
            //   .AddTypedClient<IUserClaimStore<User>, UsersClient>()
            //   .AddTypedClient<IUserLoginStore<User>, UsersClient>()
            //   .AddTypedClient<IRoleStore<Role>, RolesClient>()
            //    ;

            services.Configure<IdentityOptions>(opt =>
            {
#if true
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


            //services.AddScoped<ICartService,  InCookiesCartService>();
            services.AddScoped<ICartStore, InCookiesCartStore>();
            services.AddScoped<ICartService,  CartService>();


            #region Http Сервисы

            services.AddHttpClient("WebStoreWebAPI", client => client.BaseAddress = new(Configuration["WebAPI"]))
              .AddTypedClient<IValuesService, ValuesClient>()
              .AddTypedClient<IEmployeesData, EmployeesClient>()
              .AddTypedClient<IProductData, ProductsClient>()
              .AddTypedClient<IOrderService, OrdersClient>()
              .SetHandlerLifetime(TimeSpan.FromMinutes(5))     // Создать кеш HttpClient объектов с очисткой его по времени
               .AddPolicyHandler(GetRetryPolicy())              // Политика повторных запросов в случае если WebAPI не отвечает
               .AddPolicyHandler(GetCircuitBreakerPolicy());    // Разрушение потенциальных циклических запросов в большой распределённой системе

            static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int MaxRetryCount = 5, int MaxJitterTime = 1000)
            {
                var jitter = new Random();
                return HttpPolicyExtensions
                   .HandleTransientHttpError()
                   .WaitAndRetryAsync(MaxRetryCount, RetryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, RetryAttempt)) +
                        TimeSpan.FromMilliseconds(jitter.Next(0, MaxJitterTime)));
            }

            static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
                HttpPolicyExtensions
                   .HandleTransientHttpError()
                   .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5, TimeSpan.FromSeconds(30));

            #endregion


            services.AddSingleton<IBlogsData, InMemoryBlogData>();

            services.AddRazorPages();
            services.AddControllersWithViews
                (opt => opt.Conventions.Add(new TestControllerConvention())).AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

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

            app.UseMiddleware<ExceptionHandlingMiddleware>();



            app.UseEndpoints(endpoints => // маршруты конечный точек
            {
                              
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
