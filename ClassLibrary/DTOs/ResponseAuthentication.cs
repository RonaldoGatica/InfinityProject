using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryInfinity.DTOs
{
    public class ResponseAuthentication
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
