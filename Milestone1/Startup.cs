using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Milestone1.Data;
using Milestone1.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Milestone1
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("ConnectionName"));
            });




            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMvc(option=>option.EnableEndpointRouting=false);

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
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "account",
                    template: "{controller=Account}/{action=Login}/{id?}");
                routes.MapRoute(
                     name: "roles",
                     template: "{controller=Roles}/{action=Index}/{id?}");
                routes.MapRoute(
                     name: "users",
                     template: "{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "clients",
                    template: "{controller=Clients}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "books",
                    template: "{controller=Books}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "booknumbers",
                    template: "{controller=BookNumbers}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "authors",
                    template: "{controller=Authors}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "authorbooks",
                    template: "{controller=AuthorBooks}/{action=Index}/{id?}");
            });
        }

    
}
}
