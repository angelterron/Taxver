using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Evaluacion
    {
        public int IdEvaluacion { get; set; }
        public int? IdCliente { get; set; }
        public int? Valoracion { get; set; }
        public string Comentarios { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }

        public Usuarios IdClienteNavigation { get; set; }
        public Viaje IdEvaluacionNavigation { get; set; }
    }
}
