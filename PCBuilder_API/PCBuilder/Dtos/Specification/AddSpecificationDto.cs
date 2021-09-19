using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Specification
{
    public class AddSpecificationDto
    {
        public string Name { get; set; }

        public ProductType ProductType { get; set; }
    }
}
