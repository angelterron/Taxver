using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Taxver.Controllers
{
    public class JSONController : Controller
    {
        public String Conductores()
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var Posiciones = context.Posicionconductor.Where(pos => pos.Status == 1).ToArray();

            foreach (Posicionconductor p in Posiciones)
            {
                p.IdConductorNavigation = context.Conductor.Where(co => co.IdConductor == p.IdConductor).First();
                p.IdConductorNavigation.IdPersonaNavigation = context.Persona.Where(pe => pe.IdPersona == p.IdConductorNavigation.IdPersona).First();
                p.IdConductorNavigation.IdVehiculoNavigation = context.Vehiculo.Where(ve => ve.IdVehiculo == p.IdConductorNavigation.IdVehiculo).First();
                
            }

            var ConductoresJson = JsonConvert.SerializeObject(new
            {
                Posiciones = Posiciones
            });
            return ConductoresJson;
        }
        [HttpGet]
        public String Conductor(String correo)
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var Persona = context.Persona.Where(pe => pe.Email == correo).FirstOrDefault();
            if (Persona != null)
            {
                var Conductor = context.Conductor.Where(co => co.IdPersona == Persona.IdPersona).FirstOrDefault();
                if (Conductor != null)
                {
                    Conductor.IdPersonaNavigation = Persona;
                    Conductor.IdVehiculoNavigation = context.Vehiculo.Where(ve => ve.IdVehiculo == Conductor.IdVehiculo).First();
                    var ConductoresJson = JsonConvert.SerializeObject(Conductor);
                    return ConductoresJson;
                }
                else
                {

                    return JsonConvert.SerializeObject(null);
                }
            }
            else
            {
                return JsonConvert.SerializeObject(null); ;
            }
        }
        public String LoginApp(String Correo, String password)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var pass = getSHA1(password);
            Usuarios e = tc.Usuarios.Where(u => u.Nombre == Correo && u.Password == pass).FirstOrDefault();
            if (e != null)
            {
                e.IdPersonaNavigation = tc.Persona.Where(p => p.IdPersona == e.IdPersona).FirstOrDefault();
                var Usuario = JsonConvert.SerializeObject(e);
                return Usuario;
            }
            else
            {
                return JsonConvert.SerializeObject(null);
            }
        }        
        public ActionResult ActualizarPosicion(string lat, string lng, int idConductor)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            try
            {
                var posicion = tc.Posicionconductor.Where(posc => posc.IdConductor == idConductor).FirstOrDefault();
                if (posicion != null)
                {
                    posicion.Lat = lat;
                    posicion.Lng = lng;
                    posicion.Status = 1;
                    tc.Posicionconductor.Update(posicion);
                    tc.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception e)
            {
                return NotFound(e);
            }
            
        }
        private string getSHA1(string password)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}