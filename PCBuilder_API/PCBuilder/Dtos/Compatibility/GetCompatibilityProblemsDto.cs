using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Compatibility
{
    public class GetCompatibilityProblemsDto
    {
        public List<string> Problems { get; set; } = new List<string>();
    }
}
