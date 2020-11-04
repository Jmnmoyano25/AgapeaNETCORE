using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models
{
    public class Direcciones
    {
        #region "......propiedades de la clase....."
        public String calle { get; set; }
        public Provincia provincia { get; set; }//originalmente era un String
        public Municipio municipio { get; set; }//originalmente era un String
        public int cp { get; set; }
        #endregion
        public Direcciones()
        {
            this.provincia = new Provincia();
            this.municipio = new Municipio();
        }
    }
}
