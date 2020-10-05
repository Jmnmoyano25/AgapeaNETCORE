using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class Cliente
    {
        #region "....propiedades de la clase....."

        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String nif { get; set; }

        public credenciales credeUsuario { get; set; }

        public List<Direcciones> Misdirecciones { get; set; }

        public Cliente()
        {
            this.credeUsuario = new credenciales();
        }

        public class credenciales 
        {
            public String Email { get; set; }
            public String login { get; set; }
            public String password { get; set; }
            public String hashPassword { get; set; }
        }


        /*
         nombre
         apellidos
            colección de direcciones
            ------------------------
                    dirección
                    localidad
                    cp
                    provincia
                    municipio
         nif
         email
         login
         password
         tlfno
         fecha nacimiento
         tarjeta de debito/credito
         coleccion de pedidos   fecha compra
                                id-pedido
                                total
                                estado-pedido                   ISBN10
                                colección de libro.........>    ISBN13
                                                                Titulo
                                                                Autor
                                                                Editorial
                                                                Numero paginas
                                                                Precio
                                                                Descripcion
                                                                Imagen Libro
         **/
        #endregion

    }
    public class datosPersonales
    {

    }
    
}
