using CustomMiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare
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
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapWhen(context => IsAuthenticated(context), mapBranch =>
             {
                 mapBranch.UseMiddleware<ReportModuleMiddleWare>();
             });
            
            // Asp.net Core has provided a branching request processing method which helps in processing a request depending on a condition.
            // This is an extension method exposed by the IApplicationBuilder interface. Its MapWhen().
            app.MapWhen(context => HasReportExtension(context), mapBranch =>
             {
                 mapBranch.UseMiddleware<ReportMiddleware>();
             });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private bool IsAuthenticated(HttpContext context)
        {
            return context.User == null ? false : true;
        }

        private bool HasReportExtension(HttpContext context)
        {
            return context.Request.Path.ToString().EndsWith(".report");
        }
    }
}
