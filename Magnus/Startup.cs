using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Piranha;
using Piranha.AspNetCore.Identity.PostgreSQL;
using Piranha.Extend.Blocks;
using System.Globalization;

namespace Magnus
{
    public class Startup
    {
        /// <summary>
        /// The application config.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="env">The current hosting environment</param>
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => //CookieAuthenticationOptions
               {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login");
               });

            services.AddAuthorization(o =>
            {
                // Read secured posts
                o.AddPolicy("ReadSecuredPosts", policy =>
                {
                    policy.RequireClaim("ReadSecuredPosts", "ReadSecuredPosts");
                });
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(/*config =>
            {
                config.ModelBinderProviders.Insert(0, new Piranha.Manager.Binders.AbstractModelBinderProvider());
            }*/).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddPiranhaManagerOptions()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("kk-KZ"),
                    new CultureInfo("ru-RU")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddPiranha();
            services.AddPiranhaApplication();
            services.AddPiranhaFileStorage();
            services.AddPiranhaImageSharp();
            services.AddPiranhaManager();
            services.AddPiranhaMemoryCache();

            //
            // Setup Piranha & Asp.Net Identity with SQLite
            //
            services.AddPiranhaEF(options =>
                options.UseNpgsql(Configuration.GetConnectionString("piranha")));
            services.AddPiranhaIdentityWithSeed<IdentityPostgreSQLDb>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("piranha")));

            //
            // Setup Piranha & Asp.Net Identity with SQL Server
            //
            // services.AddPiranhaEF(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("piranha")));
            // services.AddPiranhaIdentityWithSeed<IdentitySQLServerDb>(options =>
            //     options.UseSqlServer(Configuration.GetConnectionString("piranha")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApi api)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Initialize Piranha
            App.Init(api);

            // Configure cache level
            App.CacheLevel = Piranha.Cache.CacheLevel.Basic;

            // Build content types
            var pageTypeBuilder = new Piranha.AttributeBuilder.PageTypeBuilder(api)
                .AddType(typeof(Models.BlogArchive))
                .AddType(typeof(Models.StandardPage))
                .AddType(typeof(Models.StartPage))
                .AddType(typeof(Models.NewsPage))
                .AddType(typeof(Models.ArticlePage))
                .AddType(typeof(Models.PartnersPage))
                .AddType(typeof(Models.LinksPage))
                .AddType(typeof(Models.OutcomesPage))
                .AddType(typeof(Models.ProjectsPage))
                .AddType(typeof(Models.LabsPage))
                .AddType(typeof(Models.EventPage))
                .AddType(typeof(Models.EventsPage))
                .AddType(typeof(Models.EventsMainPage));
            pageTypeBuilder.Build()
                .DeleteOrphans();
            var postTypeBuilder = new Piranha.AttributeBuilder.PostTypeBuilder(api)
                .AddType(typeof(Models.BlogPost));
            postTypeBuilder.Build()
                .DeleteOrphans();

            App.Blocks.Register<AuthorizedBlock>();

            //// Custom middleware that checks for status 401
            //app.Use(async (ctx, next) =>
            //{
            //    await next();

            //    if (ctx.Response.StatusCode == 401)
            //    {
            //        ctx.Response.Redirect("/login");
            //    }
            //});

            // Register middleware
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UsePiranha();
            app.UsePiranhaManager();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");
            });

            // Custom permissions
            App.Permissions["App"].Add(new Piranha.Security.PermissionItem
            {
                Title = "Read secured posts",
                Name = "ReadSecuredPosts"
            });
        }
    }
}
