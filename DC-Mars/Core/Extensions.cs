using System;
using System.Collections.Generic;
using System.Text;

namespace DC_Mars.Core
{
    public static class Extensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (maxLength <= 0) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}