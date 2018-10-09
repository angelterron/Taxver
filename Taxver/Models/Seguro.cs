using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Seguro
    {
        public Seguro()
        {
            FechasSeguro = new HashSet<FechasSeguro>();
        }

        public int IdSeguro { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaIncio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }

        public ICollection<FechasSeguro> FechasSeguro { get; set; }
    }
}
