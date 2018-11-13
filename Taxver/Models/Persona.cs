using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Taxver.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Conductor = new HashSet<Conductor>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public ICollection<Conductor> Conductor { get; set; }
        [JsonIgnore]
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
