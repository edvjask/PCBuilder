
using PCBuilder.Dtos.SpecificationDto;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductType ProductType { get; set; }

        public double LowestPrice { get; set; } = -1;

        public string Phone { get; set; } = "-";

        public string Location { get; set; } = "-";

        public string ImagePath { get; set; }
    }
}
