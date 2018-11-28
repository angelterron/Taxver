using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Conductor
    {
        public Conductor()
        {
            Posicionconductor = new HashSet<Posicionconductor>();
            Viaje = new HashSet<Viaje>();
        }

        public int IdConductor { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdPersona { get; set; }
        public string Foto { get; set; }
        public int? Status { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
        [JsonIgnore]
        public ICollection<Posicionconductor> Posicionconductor { get; set; }
        [JsonIgnore]
        public ICollection<Viaje> Viaje { get; set; }
    }
}
