using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgapeaNETCORE.Models;
using AgapeaNETCORE.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace AgapeaNETCORE.Controllers
{
    public class ClienteController : Controller
    {

        #region  "......PROPIEDADES DE CLASE..........."
        private IDBAcces _accesoBD;
        #endregion
        #region  "......METODOS DE CLASE..........."

            //-----inyectamos un objeto al constructor
        public ClienteController(IDBAcces accesoBD)
        {
            this._accesoBD = accesoBD;
        }

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
        public IActionResult Registro(Cliente nuevoCliente, String repassword, Boolean condicionesUso)
        {//poner un punto de parada para ver como evoluciona la pagina
            /*
            logica para:
                1º validar objeto cliente, 
                2º almacenarlo en la bd (hasheo de la password, nunca en texto plano)
                3º enivar email de activación de cuenta
                4º redireccion (o a panel o a Index de tienda) <---- crear ESTADO SESION

            */
            //1º paso, validar objeto cliente, con el MODELSTATE
            if(nuevoCliente.credeUsuario.password != repassword)//lo utilizamos para validar las contraseñas
            {
                ModelState.AddModelError("", "Las contraseñas introducidas no coinciden");
            }
            if (condicionesUso == false)
            {
                ModelState.AddModelError("", "Las condiciones han de aceptarse si o si");
            }
            if (ModelState.IsValid)
            {
                //estado de validación de todo el objeto OK, pasaria al 2º paso....

                //
                if (this._accesoBD.ResgistrarCliente(nuevoCliente))
                {
                    //insertar en BD ok, pasaria al 3ºº paso...envio de email al cliente.....
                    //4º paso redirección
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error en el proceso de datos de servidor, intentelo mas tarde....");
                    return View(nuevoCliente);
                }


                /* NO UTILIZAMOS ESTE METODO POR QUE LA BASE DE DATOS ESTA ESPECIFICADA, LO HAREMOS A TRAVES DE INTERFACES
                //objeto creado de la clase SQLServerDBAAcces para utilizar sus metodos
                SQLServerDBAAcces _accesoBD = new SQLServerDBAAcces();
                if (_accesoBD.InsertarCliente(nuevoCliente))
                {
                    //insertar en BD ok, pasaria al 3ºº paso...envio de email al cliente.....
                    //4º paso redirección
                }
                else
                {
                    ModelState.AddModelError("", "Error en el proceso de datos de servidor, intentelo mas tarde....");
                    return View(nuevoCliente);
                }*/

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
