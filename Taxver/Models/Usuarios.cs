using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public int? IdTipoUsuario { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        public int? IdPersona { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public TipoUsuario IdTipoUsuarioNavigation { get; set; }
        [JsonIgnore]
        public ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
