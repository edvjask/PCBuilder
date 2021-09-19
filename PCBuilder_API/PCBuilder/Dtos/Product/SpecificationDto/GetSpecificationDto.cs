using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCBuilder.Models;

namespace PCBuilder.Dtos.SpecificationDto
{
    public class GetSpecificationDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
