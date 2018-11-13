using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;
using Newtonsoft.Json;

namespace Taxver.Controllers
{
    public class JSONController : Controller
    {        
        public String Conductores()
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var Conductores = context.Conductor.Where(co => co.IdVehiculo != 1).ToArray();
            foreach (Conductor c in Conductores) 
            {
                c.IdPersonaNavigation = context.Persona.Where(pe => pe.IdPersona == c.IdPersona).First();
                c.IdVehiculoNavigation = context.Vehiculo.Where(ve => ve.IdVehiculo == c.IdVehiculo).First();
            }            
            var ConductoresJson = JsonConvert.SerializeObject(Conductores);
            return ConductoresJson;
        }
    }
}