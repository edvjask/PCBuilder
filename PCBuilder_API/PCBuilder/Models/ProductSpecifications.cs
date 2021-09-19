using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class ProductSpecifications
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }

        public string Value { get; set; }
    }
}
