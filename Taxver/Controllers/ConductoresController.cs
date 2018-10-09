using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Taxver.Controllers
{
    public class ConductoresController : Controller
    {
        // GET: Conductores
        public ActionResult Ver()
        {
            return View();
        }

        // GET: Conductores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Conductores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conductores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Ver));
            }
            catch
            {
                return View();
            }
        }

        // GET: Conductores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Conductores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Ver));
            }
            catch
            {
                return View();
            }
        }

        // GET: Conductores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Conductores/Delete/5
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