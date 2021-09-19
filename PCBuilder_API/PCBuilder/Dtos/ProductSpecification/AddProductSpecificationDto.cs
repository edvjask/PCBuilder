using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.ProductSpecification
{
    public class AddProductSpecificationDto
    {
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
        public string Value { get; set; }
    }
}
