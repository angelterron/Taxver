using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Viajeposicion
    {
        public int IdViajePosicion { get; set; }
        public int? Lat { get; set; }
        public int? Lng { get; set; }
        public int IdViaje { get; set; }

        public Viaje IdViajeNavigation { get; set; }
    }
}
