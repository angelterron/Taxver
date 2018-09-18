using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Mantenimiento = new HashSet<Mantenimiento>();
            Persona = new HashSet<Persona>();
        }

        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int? Numero { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        public int? IdSeguro { get; set; }

        public Seguro IdSeguroNavigation { get; set; }
        public ICollection<Mantenimiento> Mantenimiento { get; set; }
        public ICollection<Persona> Persona { get; set; }
    }
}
