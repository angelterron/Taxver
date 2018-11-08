using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;

namespace Taxver.Controllers
{
    public class UnidadesController : Controller
    {
        // GET: Unidades
        public ActionResult Ver()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var list = tc.Vehiculo.Where(v => v.IdVehiculo != 1);
            foreach (Vehiculo v in list){
                v.FechasSeguro.Add(tc.FechasSeguro.Where(f => f.IdVehiculo == v.IdVehiculo).FirstOrDefault()); 
                if(v.FechasSeguro.Last() != null)
                    v.FechasSeguro.First().IdSeguroNavigation = tc.Seguro.Where(s => s.IdSeguro == v.FechasSeguro.First().IdSeguro).FirstOrDefault();                
            }
            return View(list);
        }
        public void Status(int id)
        {
            try {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = context.Vehiculo.FirstOrDefault(ve => ve.IdVehiculo == id);
            if (entity != null)
            {
                    if (entity.Status == 1)
                    {
                        entity.Status = 2;
                    }else{
                        entity.Status = 1;
                    }
                context.Vehiculo.Update(entity);
                context.SaveChanges();
            }
            }
            catch (Exception e)
            {                
            }
        }
        // GET: Unidades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Unidades/Create
        public ActionResult Create()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            ViewBag.IdSeguro = tc.Seguro.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = s.Nombre, Value = s.IdSeguro.ToString() });
            return View();
        }

        // POST: Unidades/Create
        [HttpPost]
        public ActionResult Create(Vehiculo v,int seguro, DateTime IF, DateTime VF)
        {
            try
            {
                var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                v.Status = 1;
                v.Descripcion = "";
                tc.Vehiculo.Add(v);
                tc.SaveChanges();
                if (seguro != 0)
                {
                    FechasSeguro fs = new FechasSeguro();
                    fs.IdSeguro = seguro;
                    fs.FechaInicio = IF;
                    fs.FechaFinal = VF;
                    fs.IdVehiculo = tc.Vehiculo.Last().IdVehiculo;
                    tc.FechasSeguro.Add(fs);
                    tc.SaveChanges();
                }
                return RedirectToAction(nameof(Ver));
            }
            catch (Exception e)
            {

                return Content(""+e);
            }
        }

        // GET: Unidades/Edit/5
        public ActionResult Edit(int id)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            if (tc.Vehiculo.Where(s => s.IdVehiculo == id).First() is Vehiculo v)
            {
                ViewBag.IdSeguro = tc.Seguro.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = s.Nombre, Value = s.IdSeguro.ToString()});
                //v.IdFechasSeguroNavigation = tc.FechasSeguro.Where(fs => fs.IdFechasSeguro == v.IdFechasSeguro).First();
                //v.IdFechasSeguroNavigation.IdSeguroNavigation = tc.Seguro.Where(s => s.IdSeguro == v.IdFechasSeguroNavigation.IdSeguro).First();
                return View(v);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Unidades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehiculo v)
        {
            try
            {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = context.Vehiculo.FirstOrDefault(ve => ve.IdVehiculo == v.IdVehiculo);
                if (entity != null)
                {
                    entity.Mantenimiento = v.Mantenimiento;
                    entity.Modelo = v.Modelo;
                    entity.Marca = v.Marca;
                    entity.Numero = v.Numero;
                    entity.Placa = v.Placa;                    
                    context.Vehiculo.Update(entity);                    
                    context.SaveChanges();
                }

                return RedirectToAction(nameof(Ver));
            }
            catch
            {
                return View();
            }
        }

        // GET: Unidades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Unidades/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Ver));
            }
            catch
            {
                return View();
            }
        }
    }
}