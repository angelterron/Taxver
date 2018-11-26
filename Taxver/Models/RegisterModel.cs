using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxver.Models
{
    public class RegisterModel
    {
        public string Nombre { get; set; }
        public string Apellidos {get;set;}
        [DataType(DataType.Date)]
        public DateTime? Edad { get; set; }
        public int Tipo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}
