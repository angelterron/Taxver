using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Evaluacion = new HashSet<Evaluacion>();
            Persona = new HashSet<Persona>();
            Viaje = new HashSet<Viaje>();
        }

        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public int? IdTipoUsuario { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }

        public TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public ICollection<Evaluacion> Evaluacion { get; set; }
        public ICollection<Persona> Persona { get; set; }
        public ICollection<Viaje> Viaje { get; set; }
    }
}
