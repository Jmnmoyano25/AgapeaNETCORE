using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class Libro
    {
        /*
                                                                            isbn10
                                                                            isbn13
                                            coleccion de libro------------->Titulo
                                                                            Autor
                                                                            Editorial
                                                                            NumeroPaginas
                                                                            Precio
                                                                            Descripcion
                                                                            ImagenLibro
         
         */


        #region =================Atributos============================

        private int isbn10;
        private int isbn13;
        private String titulo;
        private String autos;
        private String editoral;
        private int numeroPaginas;
        private int precio;
        private String descripcion;
        private String ImagenLibro;



        #endregion

        #region ==================Getters Setters==============


        public int Isbn10 { get => isbn10; set => isbn10 = value; }
        public int Isbn13 { get => isbn13; set => isbn13 = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autos { get => autos; set => autos = value; }
        public string Editoral { get => editoral; set => editoral = value; }
        public int NumeroPaginas { get => numeroPaginas; set => numeroPaginas = value; }
        public int Precio { get => precio; set => precio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string ImagenLibro1 { get => ImagenLibro; set => ImagenLibro = value; }
        #endregion



    }
}
