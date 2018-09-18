using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class ObjetosPerdidos
    {
        public int IdObjetosPerdidos { get; set; }
        public string Detalles { get; set; }
        public int? IdViaje { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }

        public Viaje IdViajeNavigation { get; set; }
    }
}
