using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdTipoUsuario { get; set; }
        public string NombreTipo { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        [JsonIgnore]
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
