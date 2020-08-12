using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Code;
using INYTWebsite.Model;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace INYTWebsite
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
            services.AddAuthentication("UserCookieScheme")
                .AddCookie("UserCookieScheme", options => {
                    options.LoginPath = "/Login/";
                    options.AccessDeniedPath = "/Error/AccessDenied/";
                }
            );

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<INYTContext>(options => 
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMvc().AddViewLocalization().AddJsonOptions(options => {
                var resolver = options.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    var res = (DefaultContractResolver)resolver;
                    res.NamingStrategy = null;
                }
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ServiceProviderOnly", policy => policy.RequireClaim("UserId"));
            });

            services.AddTransient<INYTContext, INYTContext>();
            services.AddTransient<AppSettings, AppSettings>();
            services.AddTransient<IEmailManager, EmailManager>();
            services.AddTransient<ModelFactory, ModelFactory>();
            services.AddTransient<Repository, Repository>();

            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
//            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");

                routes.MapRoute(
                    name: "serviceProviderAreaRoute",
                    template: "{area:exists}/{controller=serviceprovider}/{action=index}/{id?}"
                );
            });
        }
    }
}
