using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Conductor = new HashSet<Conductor>();
            Usuarios = new HashSet<Usuarios>();
            Viaje = new HashSet<Viaje>();
        }

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public ICollection<Conductor> Conductor { get; set; }
        [JsonIgnore]
        public ICollection<Usuarios> Usuarios { get; set; }
        [JsonIgnore]
        public ICollection<Viaje> Viaje { get; set; }
    }
}
