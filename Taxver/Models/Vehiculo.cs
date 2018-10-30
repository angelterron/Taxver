using System;
using System.Collections.Generic;

namespace Taxver.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Conductor = new HashSet<Conductor>();
            FechasSeguro = new HashSet<FechasSeguro>();
            Mantenimiento = new HashSet<Mantenimiento>();
        }

        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public int? Numero { get; set; }
        public int? Status { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }

        public ICollection<Conductor> Conductor { get; set; }
        public ICollection<FechasSeguro> FechasSeguro { get; set; }
        public ICollection<Mantenimiento> Mantenimiento { get; set; }
    }
}
