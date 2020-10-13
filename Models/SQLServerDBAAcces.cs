using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AgapeaNETCORE.Models.Interfaces;

namespace AgapeaNETCORE.Models
{
    public class SQLServerDBAAcces:IDBAcces
    {


        // ----------clase con metodos para acceso a tablas Clientes,Pedidos,Libros.... sobre SQLSERVER ------



        #region "...propiedades de clase..."

        #endregion

        #region "...metodos de clase..."
        public bool ComprobarCredenciales(string loginUsuario, string password)
        {
            throw new NotImplementedException();
        }

        public Cliente DevolverCliente(string emailCliente)
        {
            throw new NotImplementedException();
        }
        public bool ResgistrarCliente(Cliente nuevoCliente)
        {
            throw new NotImplementedException();
        }

        public Boolean InsertarCliente(Cliente nuevoCliente)
        {
            //operación INSERT del nuevo cliente contra tabla Clientes de la BD.....
            return true;

        }
        #endregion
    }
}
