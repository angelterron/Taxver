using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taxver.Models;
using Microsoft.AspNetCore.Authorization;

namespace Taxver.Controllers
{
    public class PrincipalController : Controller
    {
        [Authorize]
        public IActionResult Inicio()
        {
            return View();
        }

    }
}