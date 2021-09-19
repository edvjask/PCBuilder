using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class M2Size
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public M2Slot M2Slot { get; set; }
    }
}
