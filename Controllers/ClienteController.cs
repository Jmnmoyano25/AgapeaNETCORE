using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgapeaNETCORE.Models;
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

        //Primera conexion antes mientras metemos los datos, pero antes de dar al submit
        [HttpGet]
        public IActionResult Registro()
        {
            return View(new Cliente());
        }

        
        [HttpPost]
        public IActionResult Registro(Cliente nuevoCliente)
        {//poner un punto de parada para ver como evoluciona la pagina
            /*
            logica para:
                1º validar objeto cliente, 
                2º almacenarlo en la bd (hasheo de la password, nunca en texto plano)
                3º enivar email de activación de cuenta
                4º redireccion (o a panel o a Index de tienda) <---- crear ESTADO SESION

            */
            //1º paso, validar objeto cliente, con el MODELSTATE
            if (ModelState.IsValid)
            {
                //estado de validación de todo el objeto OK, pasaria al 2º paso....
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(nuevoCliente);
            }                        
        }

        #endregion
        #region  "2.......METODOS PRIVADOS..........."

        #endregion
        #endregion
    } 
    
}
