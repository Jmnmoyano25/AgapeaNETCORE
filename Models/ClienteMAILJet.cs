using AgapeaNETCORE.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace AgapeaNETCORE.Models
{
    public class ClienteMAILJet : IClienteEnvioMail
    {
        #region "......propiedades de clase....."
        //esto no se mete aqui, siempre en ficheros de configuración protegidos
        //appsettings.jason <----- fichero .json donde el motor de aps.net core recomienda
        //poner valores de configuración de acceso: a BD, a servidores de corre,
        //a servicio de google, twitter, faceboo,....

        private String UserName = "58d0931258933a0f6b3e24bbc4b874ee";
        private String SecretAPI = "8a360baf471a92711ac8b4f8c2cb7863";
        private String _nameMAILJETServer = "in-v3.mailjet.com";

        #endregion


            /*
             MAILJET

            Usuario:		58d0931258933a0f6b3e24bbc4b874ee
            Contraseña:		8a360baf471a92711ac8b4f8c2cb7863
            Servidor SMPT:	in-v3.mailjet.com
            Puerto:			587

             */


#region ".......metodos de clase....."
    public void EnviarEmail(string emailDestino, string subject, string curpoEmail)
{
            //.....hay una clase SMTPCliente que permite hacer cliente de conexion a un servidor SMTP
            //......................(para mandar correos a los clientes)

            //  creamos 
            SmtpClient _clienteCorreo = new SmtpClient();

            //  1º paso: conexión al servidor SMTP de envio de correos de MAILJET
            _clienteCorreo.Host = this._nameMAILJETServer;
            _clienteCorreo.Credentials = new NetworkCredential(this.UserName, this.SecretAPI);

            //  2º paso: envio de correo, hay que crearse un objeto de tipo MailMessage
            MailMessage _mensajeEmail = new MailMessage("jmnmoyano@hotmail.com", emailDestino);
            //el primer parametro es un email que hay que dar de alta en MILJET para poder mandar
            //emails: panel de usuario ----> Direccioones de envio <----- en tabla DIRECCIONES [+]
            //pulsas boton + y añades la deirección de email valida para mandar emails
           // _mensajeEmail

}   
#endregion


}
}
