using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Indentity;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;
using WebStore.Services.Services.InCookies;
using WebStore.Services.Services.InMemory;
using WebStore.Services.Services.InSQL;
using WebStore.Logger;

namespace WebStore.WebAPI
{
    public record Startup(IConfiguration Configuration)
    {


        public void ConfigureServices(IServiceCollection services)
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

                //case "InMemory":
                //    services.AddDbContext<WebStoreDB>(opt => opt.UseInMemoryDatabase("Webstore.db"));
                //    break;

            }

            services.AddScoped<WebStoreDbInitializer>();

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

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddScoped<IOrderService, SqlOrderService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.WebAPI", Version = "v1" });

                // c.IncludeXmlComments("WebStore.WebAPI.xml");

                const string webstore_api_xml = "WebStore.WebAPI.xml";
                const string webstore_domain_xml = "WebStore.Domain.xml";
                const string debug_path = "bin/debug/net5.0";

                if (File.Exists(webstore_api_xml))
                    c.IncludeXmlComments(webstore_api_xml);
                else if (File.Exists(Path.Combine(debug_path, webstore_api_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, webstore_api_xml));

                if (File.Exists(webstore_domain_xml))
                    c.IncludeXmlComments(webstore_domain_xml);
                else if (File.Exists(Path.Combine(debug_path, webstore_domain_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, webstore_domain_xml));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
