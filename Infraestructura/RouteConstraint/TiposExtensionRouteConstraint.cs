using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Infraestructura.RouteConstraint
{
    public class TiposExtensionRouteConstraint : IRouteConstraint
    {
        #region ".....propiedades de clase...."
        private string[] extensionesPosibles = { "txt", "docx", "zip", "rar" };
        #endregion

        #region "...metodos de clase...."
        public bool Match(HttpContext httpContext,
                                   IRouter route,
                                   string routeKey,
                                   RouteValueDictionary values,
                                   RouteDirection routeDirection)
        {
            // devuelve true si cumple la restriccion si en segmento extension hay un valor que esta dentro del array de extensiones

            string valorSegmento = values[routeKey] as string ?? "";
            return Array.IndexOf(extensionesPosibles, valorSegmento.ToLower()) > -1;
        }
        #endregion

    }
}
