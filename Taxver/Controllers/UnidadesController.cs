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
            return View();
        }
        public void Status(int v)
        {
            try { 
            var context = new taxverContext();
            var entity = context.Vehiculo.FirstOrDefault(ve => ve.IdVehiculo == v);
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
            taxverContext tc = new taxverContext();
            ViewBag.IdSeguro = tc.Seguro.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = s.Nombre, Value = s.IdSeguro.ToString() });
            return View();
        }

        // POST: Unidades/Create
        [HttpPost]
        public ActionResult Create(Vehiculo v)
        {
            try
            {
                taxverContext tc = new taxverContext();
                v.Status = 1;
                v.Descripcion = "";
                tc.Vehiculo.Add(v);
                tc.SaveChanges();
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
            taxverContext tc = new taxverContext();            
            if (tc.Vehiculo.Where(s => s.IdVehiculo == id).First() is Vehiculo v)
            {
                ViewBag.IdSeguro = tc.Seguro.Select(s => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = s.Nombre, Value = s.IdSeguro.ToString()});
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
                var context = new taxverContext();
                var entity = context.Vehiculo.FirstOrDefault(ve => ve.IdVehiculo == v.IdVehiculo);
                if(entity != null)
                {
                    entity.Mantenimiento = v.Mantenimiento;
                    entity.Modelo = v.Modelo;
                    entity.Numero = v.Numero;
                    entity.Placa = v.Placa;
                    entity.Status = v.Status;
                    entity.IdSeguro = v.IdSeguro;
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