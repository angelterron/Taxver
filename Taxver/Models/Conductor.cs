using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Conductor
    {
        public int IdConductor { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdPersona { get; set; }
        public string Foto { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
