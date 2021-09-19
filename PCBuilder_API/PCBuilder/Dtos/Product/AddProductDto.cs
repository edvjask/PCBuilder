using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
    }
}
