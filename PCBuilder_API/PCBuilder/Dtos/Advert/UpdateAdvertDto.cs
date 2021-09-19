using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Advert
{
    public class UpdateAdvertDto
    {
        public double? Price { get; set; }
        public string Description { get; set; }

        public bool? Used { get; set; }

        public int? ProductId { get; set; }
    }
}
