using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class Direcciones
    {
        #region "......propiedades de d la clase....."
        public String localidad { get; set; }
        public String provincia { get; set; }
        public String municipio { get; set; }
        public int cp { get; set; }
        #endregion
    }
}
