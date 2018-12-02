using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;

namespace Taxver.Controllers
{
    public class ObjetosPerdidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Ver()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var list = tc.ObjetosPerdidos;
            foreach (ObjetosPerdidos c in list)
            {
                c.IdViajeNavigation = tc.Viaje.Where(p => p.IdViaje == c.IdViaje).First();
                c.IdViajeNavigation.IdConductorNavigation = tc.Conductor.Where(p => p.IdConductor == c.IdViajeNavigation.IdConductor).First();
                c.IdViajeNavigation.IdConductorNavigation.IdPersonaNavigation = tc.Persona.Where(p => p.IdPersona == c.IdViajeNavigation.IdConductorNavigation.IdPersona).First();
                c.IdViajeNavigation.IdPersonaNavigation = tc.Persona.Where(p => p.IdPersona == c.IdViajeNavigation.IdPersona).First();
            }
            return View(list);
        }

        [Authorize]
        public void Status(int id)
        {
            try
            {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = context.ObjetosPerdidos.FirstOrDefault(co => co.IdObjetosPerdidos == id);
                if (entity != null)
                {
                    if (entity.Status == 1)
                    {
                        entity.Status = 2;
                    }
                    else
                    {
                        entity.Status = 1;
                    }
                    context.ObjetosPerdidos.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
        }

    }
}