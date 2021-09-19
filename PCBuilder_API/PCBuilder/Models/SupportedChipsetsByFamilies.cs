using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class SupportedChipsetsByFamilies
    {
        public int Id { get; set; }
        public string CoreFamily { get; set; }
        public string Chipset { get; set; }
    }
}
