using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class Specification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Multiple { get; set; }

        public ProductType ProductType { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }
    }
}
