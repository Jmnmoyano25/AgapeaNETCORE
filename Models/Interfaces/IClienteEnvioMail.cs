using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Models.Interfaces
{
    public interface IClienteEnvioMail
    {
        void EnviarEmail(String emailDestino, String subject, String curpoEmail);
    }
}
