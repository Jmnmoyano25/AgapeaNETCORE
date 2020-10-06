using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class coleccionPedidos
    {
        #region "....props de la clase....."
        public String fecha_compra { get; set; }
        public int IdPedido { get; set; }
        public double totalPedido { get; set; }
        public String estadoPedido { get; set; }

        #endregion
    }

    /* coleccion de pedidos fecha compra
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
                                                                 Imagen Libro*/

}
