using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgapeaNETCORE.Models;
using Microsoft.AspNetCore.Mvc;

using AgapeaNETCORE.Models.Interfaces;

using Microsoft.AspNetCore.Http;
using System.Text.Json;  //<-----------namespace para serializar/deserializar JSON<-->objects  


namespace AgapeaNETCORE.Controllers
{
    public class ClienteController : Controller
    {

        #region  "......PROPIEDADES DE CLASE..........."
        private IDBAccess _accesoBD;
        private IClienteEnvioMail _clienteMail;
        #endregion

        //ctor + tab pone constructor
        //-----inyectamos un objeto al constructor
        public ClienteController(IDBAccess objetoAccesoBD, IClienteEnvioMail clienteCorreo)
        {
            this._accesoBD = objetoAccesoBD;
            this._clienteMail = clienteCorreo;

        }







        #region  "......METODOS DE CLASE..........."



        #region  "1.......METODOS QUE DEVUELVEN VISTAS (PUBLICOS)..........."

        //..................... metodos para generar la vista LOGIN USUARIO .......................
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //......1º acceder a la BD y si esta todo OK, es decir, que el cliente existe
        //......2º crearemos una VARIABLE 
        public IActionResult Login(Cliente.credenciales creds)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteLogin = this._accesoBD.ComprobarCredenciales(creds.Email, creds.password);
                if (clienteLogin != null) //no se puede hacer asi ahora por que te devuelve un cliente
                {
                    HttpContext.Session.SetString("sesion-id", JsonSerializer.Serialize(clienteLogin));
                    //2º Crearme una Variable de sesion y redirigir al cliente a la pagina de inicio
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email o Password invalidas");
                    return View(creds);

                }
            }
            else
            {
                return View(creds);
            }
            
            
        }
        //.....................metodos para vista REGISTRO USUARIO .......................
        [HttpPost]
        public IActionResult Login(Cliente newcliente)
        {

            if (ModelState.IsValid)
                return View();
            else
                return View(newcliente);
        }
        


        //Primera conexion antes mientras metemos los datos, pero antes de dar al submit
        [HttpGet]
        public IActionResult Registro()
        {
            //....necesito la lista de provincias para cargarlas en la vista.....
            List<Provincia> listaProvs = this._accesoBD.DevolverProvincias();

            //pasamos la lista de provincias a la vista atraves de la colección ViewData
            //es un a colección clave-valor que usan los controladores para mandar datos a las vistas
            ViewData["listaProvincias"] = listaProvs;

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
                    //insertar en BD ok, pasaria al 3º paso...envio de email al cliente.....
                    //4º paso redirección
                    // return RedirectToAction("Index", "Home");

                    String _bodyEmail = "<p>Estimado amigo: " + nuevoCliente.Nombre + "</p>" +
                                        "<p>Para completar su registro en Agapea.com es necesario que confirmes el correo electronico</p>"+
                                        "<br>"+
                                        "<a href='https://localhost:44392/Cliente/ActivarCuenta/" + nuevoCliente.credeUsuario.Email +"' >Activar cuenta</a>"+
                                        "<p>Muchas Gracias</p><br><br><p>Atentamente Agapea.com</p>";


                    this._clienteMail.EnviarEmail(nuevoCliente.credeUsuario.Email, "Bienvenido a Agapea", _bodyEmail);
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
                ModelState.AddModelError("", "Error en el proceso de datos de servidor, intentelo mas tarde....");
                return View(nuevoCliente);
            }                        
        }

        //...................metodos para ACTIVAR CUENTA.........................
        [HttpGet]
        public void ActivarCuenta(String id)
        {
            //en el parametro id va el "email" de la cuenta del usuario a activar.....
            //llamar al cliente de acceso a BD para hacer un UPDATE en la tabla
            //"dbo.Clientes" y poner la columna CuentaActivada a "true"


            //............HACER ESTO EN CASA....................

        }
        #endregion


        #region  "2.......METODOS PRIVADOS..........."

        #endregion
        #endregion
            
    }

}
