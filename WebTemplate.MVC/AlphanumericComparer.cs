namespace WebTemplate.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class AlphanumericComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // Null checkings are necessary to prevent null refernce exceptions
            // and null cannot be passed as Regex.Match() argument
            if ((x == null) && (y == null)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Compare as numbers is logicall only if both arguments have numbers
            // at the beginning because in any other case String.Compare() will
            // compare them correct anyway ("1asd" wiil be less then "abc").
            if (this.BothArgumentsBeginWithNumber(x, y))
            {
                var numberRegex = new Regex(@"[\d]+[.][\d]+|[\d]+");

                string xMatch = numberRegex.Match(x).ToString();
                string yMatch = numberRegex.Match(y).ToString();

                double xDouble = Double.Parse(xMatch, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                double yDouble = Double.Parse(yMatch, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                // Compare double numbers
                int compareResult = xDouble.CompareTo(yDouble);

                // Return result only if arguments' number parts are not equal.
                // In that case they will be compared by String.Compare() correctly.
                if (compareResult != 0) return compareResult;
            }

            // Compare as strings
            return String.Compare(x, y, System.StringComparison.Ordinal);
        }

        private bool BothArgumentsBeginWithNumber(string x, string y)
        {
            var beginsWithNumberRegex = new Regex(@"([\d]+[.][\d]+|[\d]+)*");

            bool xBeginsWithNumber = beginsWithNumberRegex.Match(x).Length > 0;
            bool yBeginsWithNumber = beginsWithNumberRegex.Match(y).Length > 0;

            return xBeginsWithNumber && yBeginsWithNumber;
        }
    }
}