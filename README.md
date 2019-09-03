# ASP.NET Core: Criando uma Web API

## Crie o projeto e a solução

- Crie uma pasta C:\GitHub\AspNetCoreWebApi,
- Abra esta pasta no Visual Studio Code,
- Clique em Terminal e depois em New Terminal,
- Digite mkdir .\src\AspNetCoreWebApi.WebApi,
- Digite dotnet new webapp –output .\src\AspNetCoreWebApi.WebApi\,
- Digite dotnet new sln,
- Digite dotnet sln add .\src\AspNetCoreWebApi.WebApi\,
- Digite mkdir .\src\AspNetCoreWebApi.WebApi\Controllers,
- Digite mkdir .\src\AspNetCoreWebApi.WebApi\DtoModels, 

## Crie a classe PersonDtoModel

- Em DtoModels adicione PersonDtoModel.

```csharp
using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.WebApi.DtoModels
{
    public class PersonDtoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Log { get; set; }
    }
}
```

## Crie o controlador PersonController

Em Controllers adicione PersonController.

```csharp
using System;
using System.Collections.Generic;
using AspNetCoreWebApi.WebApi.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi.WebApi.Controllers
{
    public class PersonController : ControllerBase
    {
        /// <summary>
        /// Get a collection of items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("")]
        public ActionResult<IEnumerable<PersonDtoModel>> Get()
        {
            var people = new List<PersonDtoModel>();
            people.Add(new PersonDtoModel() { Id = Guid.NewGuid(),
                                              Name = "John",
                                              Log = new List<string>() { "Alter Name",
                                                                         "New log" }
                                            });

            people.Add(new PersonDtoModel() { Id = Guid.NewGuid(),
                                              Name = "Mary",
                                              Log = new List<string>() { "New register"}
                                            });
            return Ok(people);
        }
    }
}
```

## Altere a classe Startup

```csharp
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
```

## Teste o projeto

- Digite dotnet run –project .\src\AspNetCoreWebApi.WebApi\,
- Abra o navegador e acesse https://localhost:5001/api/v1/public/people,
