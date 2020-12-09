using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Microsoft.AspNetCore.Antiforgery;
using Task1_Homework.Business.Services.IServices;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Net.Http.Headers;
using WebApiContrib.Core.Formatter.Csv;
using Task1_Homework.Filters;
using AutoMapper;
using Task1_Homework.Business.Queries;
using Task1_Homework.Mapper;

namespace Task1_Homework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(opts =>
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );
         
            services.AddMvc()
                .AddXmlDataContractSerializerFormatters()
                .AddCsvSerializerFormatters()
                .AddMvcOptions(opts =>
                {
                    opts.Filters.Add(typeof(CacheFilterAttribute));
                    opts.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                        new MediaTypeHeaderValue("application/xml"));
                })
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddRazorRuntimeCompilation();

            services.AddScoped<CacheFilterAttribute>();

            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IVenueService, VenueService>();

            services.AddDbContext<ResaleContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("ResaleConnection"))
                    .EnableSensitiveDataLogging(); 
            });

            services.AddDefaultIdentity<User>()
                 .AddRoles<IdentityRole>().AddEntityFrameworkStores<ResaleContext>();
            
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.Configure<AntiforgeryOptions>(opts =>
            {
                opts.FormFieldName = "StoreSecretInput";
                opts.HeaderName = "X-CSRF-TOKEN";
                opts.SuppressXFrameOptionsHeader = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSwaggerGen(c =>
            {
                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var path = Path.Combine(AppContext.BaseDirectory, file);
                c.IncludeXmlComments(path);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddMemoryCache();

            services.Scan(scan => scan
                .FromAssemblyOf<BaseQuery>()
                .AddClasses(c => c.AssignableTo(typeof(ISortingProvider<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddAutoMapper(typeof(MappingProfile));
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

            app.UseSwagger();

            var supportedLocales = new[] { "en-US", "ru", "zh"};

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedLocales[0])
                .AddSupportedCultures(supportedLocales)
                .AddSupportedUICultures(supportedLocales);

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resale API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Event}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
