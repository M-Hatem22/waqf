using BackEgyVision.Infrastructure;
using BackEgyVision.Models;
using DinkToPdf;
using DinkToPdf.Contracts;
using EgyVisionCore.Entities.EgyVision;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
//using Microsoft.AspNetCore.Mvc.Formatters.Json;

namespace BackEgyVision
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string enUSCulture { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            //.AddEnvironmentVariables();
            Configuration = builder.Build();
            enUSCulture = "en-US";
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\AntiFrogryToken");
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                                            UnicodeRanges.Arabic }));

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            // The next line to connectt the AppSettingsModel class in the core with the ApplicationSettings file in the front end.
            services.Configure<EgyVisionCore.AppSettingsModel>(Configuration.GetSection("ApplicationSettings"));
            services.AddOptions();
            services.AddAntiforgery();

            //services.AddDNTCaptcha();

            // This line is to initiate the connection for each user request to the Saree3 DB.
            // By taking the connection string from >>> ApplicationSettings:Saree3Context
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["ApplicationSettings:EgyVisionContext"]));

            // Microsoft Membership(Identity).
            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddErrorDescriber<ArabiclanguageIdentityErrorDescriber>()
            .AddPasswordValidator<PasswordCustomValidator<AppUser>>().AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();


            // To initiate the seesion
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(double.Parse(Configuration["ApplicationSettings:SessionSeconds"].ToString()));
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "EgyVision-Back-auth";
            });

            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";

            });

            services.Configure<CookieAuthenticationOptions>(options =>
            {
                options.AccessDeniedPath = new PathString("/NotAuthorized/Index");
            });

            //services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization().AddJsonOptions(opt =>
            //{
            //    var resolver = opt.SerializerSettings.ContractResolver;
            //    if (resolver != null)
            //    {
            //        var res = resolver as DefaultContractResolver;
            //        res.NamingStrategy = null;  // <<!-- this removes the camelcasing
            //    }
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization().AddRazorRuntimeCompilation();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[]
           {
                new CultureInfo(enUSCulture),
                new CultureInfo("ar-SA")
            };

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.use();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllersWithViews();
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }
        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllerRoute(
        //            name: "default",
        //            pattern: "{controller=Home}/{action=Index}/{id?}");
        //    });
        //}
    }
}
