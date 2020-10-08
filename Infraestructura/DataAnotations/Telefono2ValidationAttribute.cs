using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AgapeaNETCORE.Infraestructura.DataAnotations
{

    public class Telefono2ValidationAttribute : Attribute, IModelValidator
    {
        #region  "......propiedades de clase........."
        public String ErrorMessage { get; set; } = "error validadcion formato telefono, debe ser: Madrid-(34) 677123456";
        #endregion

        #region  "......metodos de clase........."
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            //throw new NotImplementedException();
            // la propiedad .Model de objeto ModelValidatrionContxt <----- valor de la propiedad a validar
            String _tlfno = context.Model as String ?? "";
            Regex _patron = new Regex(@"^(Madrid|Barcelona|Sevilla)-\([0-9]{2}\) [0-9]{9}$");

            //el resultado de la validación tiene que ser un conjunto de objetos 
            //ModalValidationResult (implement interface IEnumerable <---- List, Arrays, Dictionary, ....
            if (_patron.IsMatch(_tlfno))
            {
                //validación correcta pro parte del atributo....
                return Enumerable.Empty<ModelValidationResult>();
            }
            else
            {
                /*En mas pasos:
                 * List<ModalValidationResult> _listaErrores = new List<ModelValidationResult>();
                 * ModelValidationResult _error1 = new ModelValidationResult("", this.ErrorMessage);
                 * 
                 * _listaErrores.Add(_error1);
                 * return _listaErrores;
                 */

                return new List<ModelValidationResult> { new ModelValidationResult("", this.ErrorMessage) };
            }
        }
        #endregion

    }
}
