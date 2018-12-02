using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Taxver.Models;
using System.Security.Cryptography;
using System.Text;

namespace Taxver.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string ReturnURL)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                if (ReturnURL != null)
                    ViewData.Add("ReturnURL", ReturnURL);
                else
                    ViewData.Add("ReturnURL", "");
                return View();
            }
            else
            {
                return RedirectToAction("Inicio", "Principal"); //Action,Controller
            }                

        }

        public ActionResult Login()
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterModel e, DateTime fechaN)
        {
            if(e.Password == e.PasswordAgain)
            {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                Persona person = new Persona();
                person.Nombre = e.Nombre;

                string [] apellidos = e.Apellidos.Split(" ");

                person.ApellidoPaterno = apellidos[0];
                person.ApellidoMaterno = apellidos[1];
                person.FechaNacimiento = fechaN;
                person.Telefono = e.Telefono;
                person.Email = e.Email;
                person.Status = 1;
                context.Persona.Add(person);
                context.SaveChanges();

                Usuarios user = new Usuarios();
                user.Nombre = e.Email;
                user.Password = getSHA1(e.Password);
                user.IdPersona = context.Persona.Last().IdPersona;
                user.IdTipoUsuario = 1;
                user.Status = 1;

                context.Usuarios.Add(user);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public ActionResult CreateConductor([FromBody]Usuarios e)
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            try
            {
                Usuarios user = new Usuarios();
                user.Nombre = e.Nombre;
                user.Password = getSHA1(e.Password);
                user.IdPersona = e.IdPersona;
                user.IdTipoUsuario = 2;
                user.Status = 1;
                context.Usuarios.Add(user);
                context.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult CreateCliente([FromBody]Usuarios e)
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            try
            {
                Persona per = new Persona();
                per.Nombre = e.IdPersonaNavigation.Nombre;
                per.ApellidoMaterno = e.IdPersonaNavigation.ApellidoMaterno;
                per.ApellidoPaterno = e.IdPersonaNavigation.ApellidoPaterno;
                per.Email = e.Nombre;
                per.Telefono = e.IdPersonaNavigation.Telefono;
                per.FechaNacimiento = e.IdPersonaNavigation.FechaNacimiento;
                per.Status = 1;
                context.Persona.Add(per);
                context.SaveChanges();
                Usuarios user = new Usuarios();
                user.Nombre = e.Nombre;
                user.Password = getSHA1(e.Password);
                user.IdPersona = context.Persona.LastOrDefault().IdPersona;
                user.IdTipoUsuario = 3;
                user.Status = 1;
                context.Usuarios.Add(user);
                context.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public String ActualizarPosicion([FromBody]Posicionconductor pos)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            try
            {
                var posicion = tc.Posicionconductor.FirstOrDefault(posc => posc.IdConductor == pos.IdConductor);
                if (posicion != null)
                {
                    posicion.Lat = pos.Lat;
                    posicion.Lng = pos.Lng;
                    tc.Posicionconductor.Update(posicion);
                    tc.SaveChanges();
                    return "si";
                }
                else
                {
                    return "si";
                }
            }
            catch (Exception e)
            {
                return e.Message;
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Log_in(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                string pass = getSHA1(model.Password);
                Usuarios log = tc.Usuarios.Where(p => p.Nombre == model.Nombre && p.Password == pass).FirstOrDefault();
                if (log != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.Nombre));

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync("PKAT", principal);

                    if (model.returnURL == "")
                        return Redirect(model.returnURL);
                    else
                    {
                        HttpContext.Session.SetInt32("tipo", log.IdTipoUsuario);
                        HttpContext.Session.SetInt32("id", log.IdUsuarios);
                        RedirectToAction("Inicio", "Principal");
                    }                        
                }
            }
            return RedirectToAction("Index", "Login");
        }


        // POST: Login/Logout/5
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("PKAT");
            return RedirectToAction("Index","Login");
        }
    }
}