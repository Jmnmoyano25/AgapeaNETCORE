using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AgapeaNETCORE.Controllers
{
    public class ClienteController : Controller
    {

        #region  "......PROPIEDADES DE CLASE..........."

        #endregion
        #region  "......METODOS DE CLASE..........."

       

        #region  "1.......METODOS QUE DEVUELVEN VISTAS (PUBLICOS)..........."
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
        #endregion
        #region  "2.......METODOS PRIVADOS..........."

        #endregion
        #endregion
    }
}
