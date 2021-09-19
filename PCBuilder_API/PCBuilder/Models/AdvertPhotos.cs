using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class AdvertPhotos
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Advert Advert { get; set; }
    }
}
