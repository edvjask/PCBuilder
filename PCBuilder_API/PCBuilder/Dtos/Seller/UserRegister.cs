using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCBuilder.Dtos.Seller
{
    public class UserRegister
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public string Location { get; set; }
        public string Password { get; set; }
    }
}
