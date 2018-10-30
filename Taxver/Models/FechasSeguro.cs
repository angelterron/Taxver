using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class FechasSeguro
    {
        public int IdFechasSeguro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? IdSeguro { get; set; }
        public int? IdVehiculo { get; set; }

        public Seguro IdSeguroNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
