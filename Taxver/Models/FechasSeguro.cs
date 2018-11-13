using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taxver.Models
{
    public partial class FechasSeguro
    {
        public int IdFechasSeguro { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaFinal { get; set; }
        public int? IdSeguro { get; set; }
        public int? IdVehiculo { get; set; }

        public Seguro IdSeguroNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
