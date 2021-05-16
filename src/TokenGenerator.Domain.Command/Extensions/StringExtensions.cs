using System;
using System.Collections.Generic;
using System.Linq;

namespace TokenGenerator.Domain.Command.Extensions
{
    public static class StringExtensions
    {
        public static List<int> StringToIntList(this string myString)
        {
            return myString.Select(x => 
                Convert.ToInt32(x.ToString()))
                .ToList();
        }
    }
}
