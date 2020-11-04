using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgapeaNETCORE.Infraestructura.DataAnnotations
{
    public class TelefonoValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            String _tlfno = value as String;
            Regex _patron = new Regex(@"^(Madrid|Barcelona|Sevilla)-\([0-9]{2}\) [0-9]{9}$");
            if (_patron.IsMatch(_tlfno))
            {

                //return base.IsValid(value, validationContext);
                return ValidationResult.Success;

            }
            else
            {

                return new ValidationResult("error validacion formato de telefono, deber ser: Madrid-(34) 677123456");

            }

        }

    }
}
