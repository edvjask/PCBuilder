using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Seller
{
    public class GetLoggedUser
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }
    }
}
