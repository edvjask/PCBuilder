using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; }

        public List<ProductSpecifications> ProductSpecifications { get; set; }

        public string ImagePath { get; set; }

        public List<Advert> Adverts { get; set; }
    }
}
