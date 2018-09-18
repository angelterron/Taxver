using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Seguro
    {
        public Seguro()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdSeguro { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaIncio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
