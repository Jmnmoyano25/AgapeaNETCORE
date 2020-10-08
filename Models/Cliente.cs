using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class Cliente
    {
        #region "....propiedades de la clase....."
        [Required(ErrorMessage ="nombre obligatorios")]
        public String Nombre { get; set; }

        [Required(ErrorMessage ="apellidos obligatorio")]
        public String Apellidos { get; set; }

        [Required(ErrorMessage ="NIF obligatorio")]
        [RegularExpression("^[0-9]{8}-[A-Za-z]$", ErrorMessage ="formato NIF incorrecto: 12345678-A")]
        public String nif { get; set; }

        [Required(ErrorMessage = "Telefono de contacto es obligatorio por si van mal las cosas")]
        //[Telefono2ValidationAttribute]
        public int tlfnContacto { get; set; }

        public credenciales credeUsuario { get; set; }

        public List<Direcciones> Misdirecciones { get; set; }

        #endregion
        #region ".....CONSTRUCTORES......"
        public Cliente()
        {
            this.credeUsuario = new credenciales();
            this.Misdirecciones = new List<Direcciones>();
        }
        #endregion
        #region "......METODOS........."
        public class credenciales 
        {
            /*
             * Crear un atributo de validación sobre la propiedad Email para comprobar si existe o no ese email, en la BD,
             * si ya existiera devilver mensaje de error y hacer modelo invalido....
             */
            public String Email { get; set; }
            public String login { get; set; }
            public String password { get; set; }
            public String hashPassword { get; set; }
        }
        #endregion

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


    }
    public class datosPersonales
    {

    }
    
}
