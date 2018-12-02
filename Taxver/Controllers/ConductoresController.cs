using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Taxver.Models;

namespace Taxver.Controllers
{
    public class ConductoresController : Controller
    {
        private Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment _env;

        public ConductoresController(HostingEnvironment env)
        {
            _env = env;

        }


        [Authorize]
        public ActionResult Ver()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            var list = tc.Conductor;
            foreach (Conductor c in list)
            {
                c.IdPersonaNavigation = tc.Persona.Where(p => p.IdPersona == c.IdPersona).First();
                c.IdVehiculoNavigation = tc.Vehiculo.Where(v => v.IdVehiculo == c.IdVehiculo).First();
            }
            return View(list);
        }

        [Authorize]
        public void Status(int id)
        {
            try
            {
                var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = context.Conductor.FirstOrDefault(co => co.IdConductor == id);
                var entity2 = context.Posicionconductor.FirstOrDefault(pos => pos.IdConductor == id);
                if (entity != null)
                {
                    if (entity.Status == 1)
                    {
                        entity2.Status = 2;
                        entity.Status = 2;
                    }
                    else
                    {
                        entity2.Status = 1;
                        entity.Status = 1;
                    }
                    context.Conductor.Update(entity);
                    context.Posicionconductor.Update(entity2);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }


        [Authorize]
        public ActionResult Create()
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            ViewBag.IdVehiculo = tc.Vehiculo.Where(ve => ve.Status == 1 && ve.IdVehiculo != 1).Select(v => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = v.Marca +" "+v.Modelo+" "+v.Numero, Value = v.IdVehiculo.ToString() });            
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Conductor c,DateTime fechaN,IFormFile file)
        {
            try
            {

                var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
                var entity = tc.Vehiculo.FirstOrDefault(ve => ve.IdVehiculo == c.IdVehiculo);
                if (entity != null)
                {
                    entity.Status = 3;
                    tc.Vehiculo.Update(entity);
                    tc.SaveChanges();
                }
                c.IdPersonaNavigation.FechaNacimiento = fechaN;
                c.IdPersonaNavigation.Status = 1;                
                tc.Persona.Add(c.IdPersonaNavigation);
                tc.SaveChanges();
                try
                {
                    c.IdPersonaNavigation.IdPersona = tc.Persona.Last().IdPersona;
                    var path = _env.WebRootPath;
                    var filePath = System.IO.Path.Combine(path, "Fotos");
                    if (!System.IO.Directory.Exists(filePath))
                        System.IO.Directory.CreateDirectory(filePath);
                    var filecomp = c.IdPersonaNavigation.IdPersona + c.IdPersonaNavigation.Nombre + c.IdPersonaNavigation.ApellidoPaterno + c.IdPersonaNavigation.ApellidoMaterno + ".jpg";
                    var fileName = filePath + System.IO.Path.DirectorySeparatorChar + filecomp;
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    c.Foto = "Fotos/" + filecomp;
                }
                catch(Exception e)
                {
                    return Content("" + e);
                }                
                c.IdPersona = tc.Persona.Last().IdPersona;
                c.Status = 1;
                c.Tarifa = 9;                
                tc.Conductor.Add(c);
                tc.SaveChanges();
                Posicionconductor pos = new Posicionconductor();
                pos.Lat = "0";
                pos.Lng = "0";
                pos.Status = 2;
                pos.IdConductor = tc.Conductor.LastOrDefault().IdConductor;
                tc.Posicionconductor.Add(pos);
                tc.SaveChanges();
                try
                {                    
                }
                catch (Exception e)
                {
                    return Content("" + e);
                }
                return RedirectToAction(nameof(Ver));
            }
            catch (Exception e)
            {
                return Content("" + e);
            }
        }

        // GET: Conductores/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var tc = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            if (tc.Conductor.Where(s => s.IdConductor == id).First() is Conductor c)
            {
                ViewBag.IdVehiculo = tc.Vehiculo.Where(v => v.Status == 1 || c.IdVehiculo == v.IdVehiculo).Where(ve => ve.IdVehiculo != 1 ).Select(v => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = v.Marca +" " +v.Modelo +" "+v.Numero, Value = v.IdVehiculo.ToString() });                
                c.IdPersonaNavigation = tc.Persona.Where(p => p.IdPersona == c.IdPersona).First();
                
                return View(c);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Conductor c, IFormFile file)
        {
            var context = HttpContext.RequestServices.GetService(typeof(taxverContext)) as taxverContext;
            try
            {
                // TODO: Add update logic here
                if (file != null)
                {
                    var path = _env.WebRootPath;
                    String existingimage = context.Conductor.Where(nc => c.IdConductor == nc.IdConductor).Last().Foto;
                    String[] File = existingimage.Split("/");
                    var filePath = System.IO.Path.Combine(path, "Fotos");
                    
                    var fileName = filePath + System.IO.Path.DirectorySeparatorChar + File[1];
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                var entityConductor = context.Conductor.FirstOrDefault(cd => cd.IdConductor == c.IdConductor);
                if (entityConductor != null) {
                    var entityPersona = context.Persona.FirstOrDefault(p => p.IdPersona == entityConductor.IdPersona);
                    entityPersona.Nombre = c.IdPersonaNavigation.Nombre;
                    entityPersona.ApellidoPaterno = c.IdPersonaNavigation.ApellidoPaterno;
                    entityPersona.ApellidoMaterno = c.IdPersonaNavigation.ApellidoMaterno;
                    entityPersona.FechaNacimiento = c.IdPersonaNavigation.FechaNacimiento;
                    entityPersona.Email = c.IdPersonaNavigation.Email;
                    entityPersona.Telefono = c.IdPersonaNavigation.Telefono;
                    context.Persona.Update(entityPersona);
                    if (c.IdVehiculo != 1)
                    {
                        var vehiculoN = context.Vehiculo.FirstOrDefault(v => v.IdVehiculo == c.IdVehiculo);
                        vehiculoN.Status = 3;
                        context.Vehiculo.Update(vehiculoN);
                        var PosicionConductor = context.Posicionconductor.FirstOrDefault(pos => pos.IdConductor == c.IdConductor);
                        PosicionConductor.Status = 1;
                        context.Posicionconductor.Update(PosicionConductor);
                    }
                    else
                    {
                        var PosicionConductor = context.Posicionconductor.FirstOrDefault(pos => pos.IdConductor == c.IdConductor);
                        PosicionConductor.Status = 2;
                        context.Posicionconductor.Update(PosicionConductor);
                    }
                    var vehiculoO = context.Vehiculo.FirstOrDefault(v => v.IdVehiculo == entityConductor.IdVehiculo);
                    vehiculoO.Status = 1;
                    context.Vehiculo.Update(vehiculoO);                    
                    entityConductor.IdVehiculo = c.IdVehiculo;
                    context.Conductor.Update(entityConductor);
                    context.SaveChanges();
                }                
                return RedirectToAction(nameof(Ver));
            }
            catch
            {
                return View();
            }
        }

        // GET: Conductores/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Conductores/Delete/5
        [Authorize]
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