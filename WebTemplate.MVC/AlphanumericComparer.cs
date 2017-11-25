namespace WebTemplate.MVC
{
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

                var xMatch = numberRegex.Match(x).ToString();
                var yMatch = numberRegex.Match(y).ToString();

                var xDouble = double.Parse(xMatch, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                var yDouble = double.Parse(yMatch, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                // Compare double numbers
                var compareResult = xDouble.CompareTo(yDouble);

                // Return result only if arguments' number parts are not equal.
                // In that case they will be compared by String.Compare() correctly.
                if (compareResult != 0) return compareResult;
            }

            // Compare as strings
            return string.Compare(x, y, System.StringComparison.Ordinal);
        }

        private bool BothArgumentsBeginWithNumber(string x, string y)
        {
            var beginsWithNumberRegex = new Regex(@"([\d]+[.][\d]+|[\d]+)*");

            var xBeginsWithNumber = beginsWithNumberRegex.Match(x).Length > 0;
            var yBeginsWithNumber = beginsWithNumberRegex.Match(y).Length > 0;

            return xBeginsWithNumber && yBeginsWithNumber;
        }
    }
}