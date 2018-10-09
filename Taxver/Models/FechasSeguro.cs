using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class FechasSeguro
    {
        public FechasSeguro()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdFechasSeguro { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? IdSeguro { get; set; }

        public Seguro IdSeguroNavigation { get; set; }
        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
