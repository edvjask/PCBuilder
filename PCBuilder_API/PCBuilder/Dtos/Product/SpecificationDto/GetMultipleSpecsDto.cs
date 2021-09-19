using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.SpecificationDto
{
    public class GetMultipleSpecsDto
    {
        public string Name { get; set; }
        public List<string> Value { get; set; }
    }
}
