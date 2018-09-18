using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Edad { get; set; }
        public int? Telefono { get; set; }
        public string Email { get; set; }
        public int IdUsuario { get; set; }
        public int IdVehiculo { get; set; }
        public int? Status { get; set; }

        public Usuarios IdUsuarioNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
