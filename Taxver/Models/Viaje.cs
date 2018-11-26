using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taxver.Models
{
    public partial class Viaje
    {
        public Viaje()
        {
            ObjetosPerdidos = new HashSet<ObjetosPerdidos>();
            Viajeposicion = new HashSet<Viajeposicion>();
        }

        public int IdViaje { get; set; }
        public int? IdConductor { get; set; }
        public float? Kilometros { get; set; }
        public float? Tarifa { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Fecha { get; set; }

        public Usuarios IdConductorNavigation { get; set; }
        public Evaluacion Evaluacion { get; set; }
        [JsonIgnore]
        public ICollection<ObjetosPerdidos> ObjetosPerdidos { get; set; }
        [JsonIgnore]
        public ICollection<Viajeposicion> Viajeposicion { get; set; }
    }
}
