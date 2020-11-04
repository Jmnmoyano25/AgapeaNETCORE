using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgapeaNETCORE.Models;
using AgapeaNETCORE.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgapeaNETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RESTController : ControllerBase
    {
        #region  "......propiedades de clase............."
        private IDBAccess _accesoDB;
        #endregion
        
        public RESTController(IDBAccess objetoAccesoDBinyectado)
        {
            this._accesoDB = objetoAccesoDBinyectado;
        }



        #region  "..........metodos........................."

        [HttpGet("{codpro}")]
        public List<Municipio> DevolverMunicipios(int codpro)
        {
            //metodo que se invoca por AJAX  desde el navegadro del cliente
            //cuando se selecciona una determinada provincia en el resgitro, para cargar
            //los municipios de la misma <----- necesito acceder a tabla Municipos SQLServer

            /*
             *  cliente                                        servidor as.net <------> servidor SQLServer
             * http:/localhost:xxx/Cliente/Registro ----->     controladores-vistas
             *                                      <-----      ClienteCotroller ---> Registro <-------- recupera provincias
             *                                                  controlador-rest
             *                                                  
             * (seleciona una provincia) quiero
             * cargar en cliente los municipios
             * esa provincia AJAX:
             *  http:/localhost:xxxx/api/REST/DevolverMunicipios/xxxx   ------> controlador-rest <------recupera municipios de ese codpro
             *                                                          <------   DATOS JSON
             * al recibir hay que pintarlos desde 
             * codigo javascript en dropdown municipios
             * 
             * */

            return this._accesoDB.DevolverMunicipios(codpro);
        }

        #endregion











        /*
         * Pablo prefiere las clases con nombre propios
         * 
         * 
        // GET: api/<RESTController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RESTController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<RESTController>?id=5
        [HttpGet]
        public string Get([FromQuery]string id)
        {
            return "Has mandado un la URL este valor: " + id ;
        }

        // POST api/<RESTController>
        [HttpPost]
        [Consumes("application/json")]
        public string Post([FromBody] string value)
        {
            return "Has pasado por POST al servidor este valor: " + value.ToString();
        }

        // PUT api/<RESTController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RESTController>/5
        [HttpDelete("{nif}")]
        public string Delete(int nif)
        {

            return "estas intentanod borrar de la BD el objeto con el id...  " + nif.ToString();
        }

        */
    }
}
