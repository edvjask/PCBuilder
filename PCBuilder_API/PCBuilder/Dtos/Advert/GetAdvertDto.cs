using PCBuilder.Dtos.Product;
using PCBuilder.Dtos.Seller;
using PCBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Advert
{
    public class GetAdvertDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public bool Used { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastEditedOn { get; set; }

        public bool Confirmed { get; set; }

        public string ProductName { get; set; }

        public GetUserDto Seller { get; set; }
    }
}
