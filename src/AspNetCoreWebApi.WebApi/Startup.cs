using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreWebApi.WebApi
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
            services
                .AddCors(
                    options => options
                        .AddPolicy("AllowLocalhostOrigin",
                                   builder => builder
                                       .WithOrigins("https://localhost:5001/", "http://localhost:5000/")
                                       .AllowAnyMethod()
                                       .AllowAnyHeader()))
                .AddMvc(options =>
                {
                    options.OutputFormatters.RemoveType<TextOutputFormatter>();
                    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection()
               .UseCors("AllowLocalhostOrigin")
               .UseMvc(routes =>
               {
                   routes.MapRoute("default", "api/v1/public/{controller=home}/{action=index}/{id?}");
               });
        }
    }
}