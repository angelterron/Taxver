﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;

namespace Taxver.Controllers
{
    public class MantenimientoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Ver()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var list = tc.Mantenimiento;
            foreach (Mantenimiento c in list)
            {
                c.IdVehiculoNavigation = tc.Vehiculo.Where(p => p.IdVehiculo == c.IdVehiculo).First();
            }
            return View(list);
        }

        [Authorize]
        public void Status(int id)
        {
            try
            {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = context.Mantenimiento.FirstOrDefault(co => co.IdMantenimiento == id);
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
                    context.Mantenimiento.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
        }

    }
}