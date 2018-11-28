using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Posicionconductor
    {
        public int IdPosicionConductor { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int IdConductor { get; set; }
        public int? Status { get; set; }

        public Conductor IdConductorNavigation { get; set; }
    }
}
