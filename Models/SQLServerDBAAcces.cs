using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AgapeaNETCORE.Models.Interfaces;

namespace AgapeaNETCORE.Models
{
    public class SQLServerDBAAcces : IDBAccess
    {
        //CLASES AÑADIDAS EL 20/10/2020


        // ----------clase con metodos para acceso a tablas Clientes,Pedidos,Libros.... sobre SQLSERVER ------



        #region "...propiedades de clase..."   
        private String _cadenaConexion =      @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AgapeaDBPablo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //clase para entablar la conexion con la BD sql    
        public SqlConnection conexionSQLserver;

        #endregion

        //  ------------- constructor de la clase -----------
        public SQLServerDBAAcces()
        {
            this.conexionSQLserver = new SqlConnection(this._cadenaConexion);
        }

        #region "...metodos de clase..."


        public List<Provincia> DevolverProvincias() {

            //consulta a la BD de SqlServer tabla provincias....
            // - abro conexión con objeto SqlConnection a la BD del servidor
            // - Lanzo select
            // - genero de cada fila de la select de la tabla Provincias un objeto del Modelo Provincia
            // - lo meto en la lista y la devuelvo

            // 1º abro la conexión
            this.conexionSQLserver.Open();

            // 2º Lanzo la select
            SqlCommand _selectProvincias = new SqlCommand();
            _selectProvincias.Connection = this.conexionSQLserver;
            _selectProvincias.CommandText = "SELECT * FROM dbo.Provincias";

            //para SELECTS insert, input, etc (no devuelven filas)  metodo .ExecuteNonQuery();
            //para SELECTS que devuelven cursores (devuelven filas) metodo .ExecuteReader();

            List<Provincia> listaProvs = new List<Provincia>(); 

            /* Esto no se hace así, es solo para probar....
            {new Provincias { CodPro = 28, NombreProvincia = "Madrid"  },
             new Provincias { CodPro = 41, NombreProvincia = "Sevilla"  }};
            */
            //me recorro este cursor, y por cada fila del vursor me creo un objeto de tipo Provincia y lo añado a la lista:listaProvs
            SqlDataReader _reusltadosSelect = _selectProvincias.ExecuteReader();

            //---------------------------------------
            this.conexionSQLserver.Close();

            return listaProvs;
        }



        public List<Municipio> DevolverMunicipios(int codpro)
        {
            throw new NotImplementedException();
        }



        #region "...metodos heredados de la interface......"
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
        #endregion
    }
}
