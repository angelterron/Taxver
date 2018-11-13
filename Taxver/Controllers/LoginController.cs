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
        public ActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Log_in(Usuarios model)
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

                    return RedirectToAction("Inicio","Principal"); //Action,Controller
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