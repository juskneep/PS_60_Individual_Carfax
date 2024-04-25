using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PS_Carfax.Data.Helpers
{
    public class ValidVINAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null values are handled by the [Required] attribute
            }

            var vin = value.ToString();

            // Check VIN length
            if (vin.Length != 17)
            {
                return false;
            }

            // VIN should contain only uppercase letters and digits
            if (!Regex.IsMatch(vin, "^[A-HJ-NPR-Z0-9]*$"))
            {
                return false;
            }

            // Check VIN checksum
            int[] weights = { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] transliterations = { 1, 2, 3, 4, 5, 6, 7, 8, 0, 1, 2, 3, 4, 5, 0, 7, 8, 9, 0, 2, 3, 4, 5, 6, 7, 8, 9 };
            int checksum = 0;

            for (int i = 0; i < vin.Length; i++)
            {
                char c = vin[i];
                int digitValue = char.IsDigit(c) ? c - '0' : transliterations[c - 'A'];
                checksum += digitValue * weights[i];
            }

            int remainder = checksum % 11;

            if (remainder == 10)
            {
                return vin[8] == 'X';
            }

            return remainder == vin[8] - '0';
        }
    }
}
