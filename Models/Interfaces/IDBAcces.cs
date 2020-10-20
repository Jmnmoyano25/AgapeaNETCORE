using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models.Interfaces
{
    public interface IDBAcces
    {
        
        //Es mejor poner que te devuelva el numero de filas
        Boolean ResgistrarCliente(Cliente nuevoCliente);

        List<Provincias> DevolverProvincias();

        
        Cliente DevolverCliente(String emailCliente);
        Boolean ComprobarCredenciales(String loginUsuario, String password);

        
    }
}
