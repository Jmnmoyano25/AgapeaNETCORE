using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models.Interfaces
{
    public interface IDBAccess
    {
        
        //Es mejor poner que te devuelva el numero de filas
        Boolean ResgistrarCliente(Cliente nuevoCliente);

        List<Provincia> DevolverProvincias();

        List<Municipio> DevolverMunicipios(int codpro);



        Cliente DevolverCliente(String emailCliente);
        Boolean ComprobarCredenciales(String loginUsuario, String password);

        
    }
}
