using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Taxver.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }
    }
}