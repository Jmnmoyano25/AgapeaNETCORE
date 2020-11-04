using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using AgapeaNETCORE.Models.Interfaces;
using BCrypt.Net;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.VisualBasic;

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

            List<Provincia> listaProvs = new List<Provincia>()

            // Esto no se hace así, es solo para probar....
                {new Provincia { CodPro = 28, NombreProvincia = "Madrid"  },
                 new Provincia { CodPro = 41, NombreProvincia = "Sevilla"  }};
            
            
            SqlDataReader _reusltadosSelect = _selectProvincias.ExecuteReader();
            // me recorro este cursor, y por cada fila del cursor me creo un objeto de tipo Provincia
            // y lo añado a la lista:listaProvs 
            //                   LO TENEMOS QUE HACER NOSOTROS.....


            //---------------------------------------
            this.conexionSQLserver.Close();

            return listaProvs;
        }



        public List<Municipio> DevolverMunicipios(int codprov)
        {
            this.conexionSQLserver.Open();

            SqlCommand _selectMuni = new SqlCommand();
            _selectMuni.Connection = this.conexionSQLserver;
            // _selectMuni.CommandText = "select * from dbo.Municipios where CodPro="+ codpro + " order by NombreMunicipio"
            //mal por que poner el parametro codpro asi, es susceptible de que te inyecten codigo malicioso
            _selectMuni.CommandText = "select * from dbo.Municipios where CodPro=@codigoprov order by NombreMunicipio";
            _selectMuni.Parameters.AddWithValue("@codigoprov", codprov);

            // ejecutamos la sentencia
            SqlDataReader _resultado = _selectMuni.ExecuteReader();

            // ---------------recorrer datareader y por cada fila crear un objeto municipio y 
            // ---------------añadirlo a la lista a devolver (hacerlo con bucle while not fin cursor.....)

            //List<Municipio> _listaMunis = new List<Municipio>();
            //  bucle while.....
            //return _listaMunis;
            //---------------------------------------------------------------------------------------------
            return _resultado
                .Cast<IDataRecord>()
                .Select(fila => new Municipio()
                        {
                            CodMun=System.Convert.ToInt32(fila["CodMun"]),
                            CodPro=System.Convert.ToInt32(fila["Codpro"]),
                            NombreMunicipio=fila["NombreMunicipio"].ToString()
                        }
                )
                .ToList<Municipio>();


        }



        #region "...metodos heredados de la interface......"
        public Cliente ComprobarCredenciales(string loginUsuario, string password)
        {
            this.conexionSQLserver.Open();

            SqlCommand _buscaUser = new SqlCommand();
            _buscaUser.Connection = this.conexionSQLserver;
            _buscaUser.CommandText = "SELECT * FROM dbo.Clientes where Email = @Email AND HashPassword = @hash AND CuentaActivada=1";
            _buscaUser.Parameters.AddWithValue("@Email", loginUsuario);
           // _buscaUser.Parameters.AddWithValue("@hash", BCrypt.Net.BCrypt.HashPassword(password));
           // no funciona bien, genera una hash diferente cada vez

            SqlDataReader _resultado = _buscaUser.ExecuteReader();
            if (_resultado.HasRows)
            {
                //tendria que leer el cursor y generme ese objeto de tipo cliente con un bucle
                Cliente _clienteADevolver = new Cliente();
                while (_resultado.Read())
                {
                    _clienteADevolver.nif = ((IDataRecord)_resultado)["NIF"].ToString();
                    _clienteADevolver.Nombre = ((IDataRecord)_resultado)["Nombre"].ToString();
                    _clienteADevolver.Apellidos = ((IDataRecord)_resultado)["Apellidos"].ToString();
                    _clienteADevolver.credeUsuario.Email = ((IDataRecord)_resultado)["Email"].ToString();
                    _clienteADevolver.tlfnContacto = System.Convert.ToInt16(((IDataRecord)_resultado)["TelefonoContacto"]);
                    if(BCrypt.Net.BCrypt.Verify(password, ((IDataRecord)_resultado)["HashPassword"].ToString()))
                    {
                        return null;
                    }
                    //....dos opciones, la direccion no la recupeo hasta qu en ocompro nada...o la recupero
                    // ahora y la meto ya en la variable de sesión....Para luego tenerla en caso de necesitarla
                    // a la hora de comprar el producto
                    SqlCommand _selectDireccion = new SqlCommand();
                    _selectDireccion.Connection = this.conexionSQLserver;
                    _selectDireccion.CommandText = $"SELECT * FROM dbo.Direcciones WHERE idDireccion = 'Principal-{_clienteADevolver.nif}'";

                    SqlDataReader _resultadoDir = _selectDireccion.ExecuteReader();

                    Direcciones _direccionCliente = new Direcciones();
                    while (_resultadoDir.Read())
                    {
                        _direccionCliente.calle = ((IDataRecord)_resultadoDir)["Calle"].ToString();
                        _direccionCliente.cp = System.Convert.ToInt16(((IDataReader)_resultadoDir)["CP"]);
                        _direccionCliente.provincia.CodPro = System.Convert.ToInt16(((IDataReader)_resultadoDir)["Codigo_Provincia"]);
                        _direccionCliente.municipio.CodMun = System.Convert.ToInt16(((IDataReader)_resultadoDir)["Codigo_Municipio"]);
                    }
                    _clienteADevolver.Misdirecciones.Add(_direccionCliente);
                }
                return _clienteADevolver;
            }
            else
            {
                return null;
            }



            return _resultado;
        }

        public Cliente DevolverCliente(string emailCliente)
        {
            throw new NotImplementedException();
        }
        public bool ResgistrarCliente(Cliente nuevoCliente)
        {
            try
            {
                this.conexionSQLserver.Open();
                SqlCommand _insertarClientes = new SqlCommand();
                _insertarClientes.Connection = this.conexionSQLserver;
                _insertarClientes.CommandText = "insert into dbo.Clientes values(@NIF," + "@NOmbre," + "@Apellidos," + "@Email," + "@Hash," + "@login," + "@IdDireccion," + "@Telefono," + "@Activa," + "@Descripcion," + "@Avatar)";

                _insertarClientes.Parameters.AddWithValue("@NIF", nuevoCliente.nif);
                _insertarClientes.Parameters.AddWithValue("@Nombre", nuevoCliente.Nombre);
                _insertarClientes.Parameters.AddWithValue("@Apellidos", nuevoCliente.Apellidos);
                _insertarClientes.Parameters.AddWithValue("@Email", nuevoCliente.credeUsuario.Email);

                //-----generamos el hash para la password de la cuenta.....
                String _hash = BCrypt.Net.BCrypt.HashPassword(nuevoCliente.credeUsuario.password);
                _insertarClientes.Parameters.AddWithValue("@Hash", _hash);

                _insertarClientes.Parameters.AddWithValue("@login", nuevoCliente.credeUsuario.login);
                _insertarClientes.Parameters.AddWithValue("@IdDireccion", "Principal-" + nuevoCliente.nif);
                _insertarClientes.Parameters.AddWithValue("@Telefono", nuevoCliente.tlfnContacto);
                _insertarClientes.Parameters.AddWithValue("@Activa", false);
                _insertarClientes.Parameters.AddWithValue("@Descripcion", "");
                _insertarClientes.Parameters.AddWithValue("@Avatar", "");

                //el ExecuteNonQuery me devuelve mas de una fila por el boolean entraria a true
                int _filasInsertClientes = _insertarClientes.ExecuteNonQuery();
                if (_filasInsertClientes == 1)
                {
                    SqlCommand _insertarDirecciones = new SqlCommand();
                    _insertarDirecciones.Connection = this.conexionSQLserver;
                    _insertarDirecciones.CommandText = "INSERT INTO dbo.Direcciones VALUES (@Id,@CodPro,@Calle,@CP)";
                    _insertarDirecciones.Parameters.AddWithValue("@Id", "Principal-" + nuevoCliente.nif);
                    _insertarDirecciones.Parameters.AddWithValue("@CodPro", nuevoCliente.Misdirecciones[0].provincia.CodPro);
                    _insertarDirecciones.Parameters.AddWithValue("@ConMun", nuevoCliente.Misdirecciones[0].municipio.CodMun);
                    _insertarDirecciones.Parameters.AddWithValue("@Calle", nuevoCliente.Misdirecciones[0].calle);
                    _insertarDirecciones.Parameters.AddWithValue("@CP", nuevoCliente.Misdirecciones[0].cp);

                    int _filasInsertarDirecciones = _insertarDirecciones.ExecuteNonQuery();
                    if (_filasInsertarDirecciones == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }else
                {
                    return false;
                }


                //.....falta hacer el insert en la tabla de direcciones.........
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                this.conexionSQLserver.Close();
            }



            
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
