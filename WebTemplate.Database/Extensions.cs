using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTemplate.Database
{
    public static class Extensions
    {
        private static string GenerateRandomString(Random charRandom, Random caseRandom, int length)
        {
            var builder = new StringBuilder();
            
            for (int i = 0; i < length; i++)
            {
                int @case = caseRandom.Next(2) == 0 ? 65 : 97;
                char @char = Convert.ToChar(charRandom.Next(@case, @case + 26));

                builder.Append(@char);
            }

            return builder.ToString();
        }

        public static string NextString(this Random random, int lengh = 1)
        {
            return GenerateRandomString(random, random, lengh);
        }
    }
}
