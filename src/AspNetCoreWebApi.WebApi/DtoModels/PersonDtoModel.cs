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