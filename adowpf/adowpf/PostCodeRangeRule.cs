using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using AdoGemeenschap;

namespace adowpf
{
    public class PostCodeRangeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureinfo)
        {
            int postcode = 0;

            if ( value is BindingGroup )
                { postcode = ((Brouwer)(value as BindingGroup).Items[0]).PostCode;  }
            else
            {
                if (!int.TryParse(value.ToString(), out postcode))
                {
                    return new ValidationResult(false, "Geen getal als postcode ingegeven");
                }
            }
            
                if ((postcode <1000 ) || (postcode >9999) )
                {
                    return new ValidationResult(false,"Ingegeven postcode lag niet tussen 1000 en 9999");
                }
                else
                { return ValidationResult.ValidResult; }
            
        }
    }
}
