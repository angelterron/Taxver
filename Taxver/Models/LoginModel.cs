using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxver.Models
{
    public class LoginModel
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string returnURL { get; set; }
    }
}
