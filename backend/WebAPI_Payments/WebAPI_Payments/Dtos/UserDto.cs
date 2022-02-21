using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Payments.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public double Budget { get; set; }
        public bool IsVerified { get; set; }
    }
}
