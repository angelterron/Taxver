using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Mantenimiento
    {
        public int IdMantenimiento { get; set; }
        public string Detalles { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        public int? IdVehiculo { get; set; }

        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
