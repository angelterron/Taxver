using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Viajeposicion
    {
        public int IdViajePosicion { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int IdViaje { get; set; }
        public int? IdTipo { get; set; }

        public Viaje IdViajeNavigation { get; set; }
    }
}
