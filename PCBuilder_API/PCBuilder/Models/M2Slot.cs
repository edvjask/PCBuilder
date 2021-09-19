using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class M2Slot
    {
        public int Id { get; set; }

        public string KeyType { get; set; }

        public List<M2Size> Sizes { get; set; } 

        public M2SlotCollection M2SlotCollection { get; set; }
    }
}
