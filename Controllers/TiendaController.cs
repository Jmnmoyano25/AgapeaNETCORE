using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AgapeaNETCORE.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
