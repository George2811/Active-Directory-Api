using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryEmulatorApi.Domain.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Phone { get; set; }
        public string Image { get; set; }

    }
}
