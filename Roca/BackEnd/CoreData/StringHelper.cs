using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Cno.Roca.CoreData
{
    public static class StringHelper
    {
        public static bool Contains(this string obj, string value, StringComparison comparisonType)
        {
            return obj.IndexOf(value, comparisonType) >= 0;
        }

        public static bool ContainsCaseInsensitive(this string str, string value)
        {
            return (str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public static string RemoveDiacritics(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
