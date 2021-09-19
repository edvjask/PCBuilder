using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.PartsList
{
    public class PartListDto
    {
        public int? ProcessorId { get; set; }
        public int? CpuCoolerId { get; set; }
        public int? MotherboardId { get; set; }
        public int? MemoryId { get; set; }
        public int? GpuId { get; set; }

        public int? StorageId { get; set; }
        public int? CaseId { get; set; }
        public int? PSUId { get; set; }

    }
}
