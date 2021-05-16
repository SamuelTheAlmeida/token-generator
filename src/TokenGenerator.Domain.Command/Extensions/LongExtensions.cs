using System;

namespace TokenGenerator.Domain.Command.Extensions
{
    public static class LongExtensions
    {
        public static string GetLastDigitsString(this long myLong, int length)
        {
            var myString = myLong.ToString();
            var lastFourDigits = myString.Substring(Math.Max(0, myString.Length - length));
            return lastFourDigits;
        }
    }
}
