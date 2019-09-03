using System;
using System.Collections.Generic;
using AspNetCoreWebApi.WebApi.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi.WebApi.Controllers
{
    [Route("api/v1/public/people")]
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