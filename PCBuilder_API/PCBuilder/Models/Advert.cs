using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class Advert
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public bool Used { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastEditedOn { get; set; }

        public bool Confirmed { get; set; }

        public Product Product { get; set; }

        public User Seller { get; set; }

        public List<AdvertPhotos> AdvertPhotos { get; set; }
    }
}
