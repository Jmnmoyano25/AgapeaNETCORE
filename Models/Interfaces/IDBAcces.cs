using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models.Interfaces
{
    public interface IDBAcces
    {
        //Es mejor poner que te devuelva el numero de filas
        Boolean ResgistrarCliente(Cliente nuewcliente);
        Cliente DevolverCliente(String emailCliente);
        Boolean comprobarCredenciales(String loginUsuario, String password);
    }
}
