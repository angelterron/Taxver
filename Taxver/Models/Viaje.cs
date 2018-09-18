using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Viaje
    {
        public Viaje()
        {
            ObjetosPerdidos = new HashSet<ObjetosPerdidos>();
        }

        public int IdViaje { get; set; }
        public int? IdConductor { get; set; }
        public float? Kilometros { get; set; }
        public float? Tarifa { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }

        public Usuarios IdConductorNavigation { get; set; }
        public Evaluacion Evaluacion { get; set; }
        public ICollection<ObjetosPerdidos> ObjetosPerdidos { get; set; }
    }
}
