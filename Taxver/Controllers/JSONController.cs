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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;

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
        [HttpGet]
        public String Viajes(int idPersona)
        {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var viaje = context.Viaje.Where(vi => vi.IdPersona == idPersona).ToArray();
            if (viaje != null)
            {
                foreach (Viaje v in viaje)
                {
                    v.IdConductorNavigation = context.Conductor.Where(c => c.IdConductor == v.IdConductor).FirstOrDefault();
                    if(v.IdConductorNavigation != null)
                    {
                        v.IdConductorNavigation.IdPersonaNavigation = context.Persona.FirstOrDefault(p => p.IdPersona == v.IdConductorNavigation.IdPersona);
                    }
                }
            }

            var ViajesJson = JsonConvert.SerializeObject(new
            {
                Viajes = viaje
                });
                return ViajesJson;
        }
        public String ObjetosPerdidos(int idPersona)
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var objetosPerdidos = context.ObjetosPerdidos.ToArray();

            for (int i = 0; i < objetosPerdidos.Length; i++)
            {
                objetosPerdidos[i].IdViajeNavigation = context.Viaje.FirstOrDefault(v => v.IdViaje == objetosPerdidos[i].IdViaje);
            }
            var obs = objetosPerdidos.Where(obj => obj.IdViajeNavigation.IdPersona == idPersona).ToArray();
            var ObjetosPerdidos = JsonConvert.SerializeObject(new
            {
                ObjetosPerdidos = obs
            });
            return ObjetosPerdidos;
        }
        public String LoginApp(String Correo, String password, String phoneID )
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var pass = getSHA1(password);
            Usuarios e = tc.Usuarios.Where(u => u.Nombre == Correo && u.Password == pass).FirstOrDefault();
            if (e != null)
            {
                if(e.PhoneId != phoneID)
                {
                    e.PhoneId = phoneID;
                    tc.Usuarios.Update(e);
                    tc.SaveChanges();
                }                
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
        [HttpGet]
        public ActionResult cambiarStatus(int idConductor)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var posicionConductor = tc.Posicionconductor.LastOrDefault(pos => pos.IdConductor == idConductor);
            if (posicionConductor != null)
            {
                if (posicionConductor.Status == 1)
                    posicionConductor.Status = 2;
                else
                    posicionConductor.Status = 1;

                tc.Posicionconductor.Update(posicionConductor);
                tc.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<NotificationOutcome> Pedir(int idConductor,int idPersona,String LatO, String LngO, String LatD, String LngD)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var conductor = tc.Conductor.FirstOrDefault(c => c.IdConductor == idConductor);
            if(conductor != null)
            {
                var user = tc.Usuarios.FirstOrDefault(u => u.IdPersona == conductor.IdPersona);
                if(user != null)
                {
                    Viaje viaje = new Viaje();
                    viaje.IdConductor = idConductor;
                    viaje.IdPersona = idPersona;
                    viaje.Fecha = DateTime.Today;
                    viaje.Tarifa = conductor.Tarifa;
                    viaje.Status = 1;
                    viaje.Kilometros = 10;
                    tc.Viaje.Add(viaje);
                    tc.SaveChanges();
                    var lastViaje = tc.Viaje.LastOrDefault();
                    Viajeposicion v1 = new Viajeposicion();
                    v1.Lat = LatO;
                    v1.Lng = LngO;
                    v1.IdTipo = 1;
                    v1.IdViaje = lastViaje.IdViaje;
                    Viajeposicion v2 = new Viajeposicion();
                    v1.Lat = LatD;
                    v1.Lng = LngD;
                    v1.IdTipo = 2;
                    v1.IdViaje = lastViaje.IdViaje;
                    tc.Viajeposicion.Add(v1);
                    tc.Viajeposicion.Add(v2);
                    var nhcs = "Endpoint=sb://taxver.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Ck5pEUQFV+m1GffXWI7cbZPFdORdukjmMJLF4Us3EDI=";
                    var hubName = "NotificactionHubOverFirebase";
                    var payload = "{\"data\": { \"message\" : \"" + user.PhoneId + "\"} }";
                    var hub = NotificationHubClient.CreateClientFromConnectionString(nhcs, hubName);

                    return await hub.SendGcmNativeNotificationAsync(payload);
                }
            }
            return null;
        }
        public async Task<NotificationOutcome> Terminar(int idConductor)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var conductor = tc.Conductor.FirstOrDefault(c => c.IdConductor == idConductor);
            
            if (conductor != null)
            {
                var user = tc.Usuarios.FirstOrDefault(u => u.IdPersona == conductor.IdPersona);
                if(user != null)
                {
                    var nhcs = "Endpoint=sb://taxver.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Ck5pEUQFV+m1GffXWI7cbZPFdORdukjmMJLF4Us3EDI=";
                    var hubName = "NotificactionHubOverFirebase";
                    var payload = "{\"data\": { \"message\" : \"" + user.PhoneId + "\"} }";
                    var hub = NotificationHubClient.CreateClientFromConnectionString(nhcs, hubName);

                    return await hub.SendGcmNativeNotificationAsync(payload);
                }                
            }
            return null;
        }

    }
}