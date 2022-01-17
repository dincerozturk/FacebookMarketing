using System;
using System.Globalization;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public static class NumberHelper
    {
        public static double? TryConvertStringDoubleNull(string input)
        {
            double? result = null;

            try
            {
                if (input == "NaN")
                {
                    return result;
                }
                if (!String.IsNullOrEmpty(input))
                {
                    result = double.Parse(input);
                }
            }
            catch (Exception e)
            {
                // ignored
                return null;
            }

            return result;
        }

        public static long? TryConvertStringLongNull(string input)
        {
            long? result = null;
            try
            {
                if (!String.IsNullOrEmpty(input))
                {
                    result = long.Parse(input);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// (19950000.0).ToString("N",new CultureInfo("en-US")) = 19,950,000.00
        /// (19950000.0).ToString("N",new CultureInfo("is-IS")) = 19.950.000,00
        /// </summary>
        /// <param name="numberInput"></param>
        /// <returns></returns>
        public static string FormatNumberThousandSeparator(this int numberInput, string charater = ".")
        {
            if (charater == ".")
            {
                return numberInput.ToString("N", new CultureInfo("is-IS"));
            }

            if (charater == ",")
            {
                return numberInput.ToString("N", new CultureInfo("en-US"));
            }

            return String.Empty;
        }
    }
}
