using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Models
{
    public class M2SlotCollection
    {
        public int Id { get; set; }
        public int moboId { get; set; }

        List<M2Slot> Slots { get; set; }
    }
}
