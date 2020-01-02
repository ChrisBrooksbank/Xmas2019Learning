using System;

namespace HighPerformanceSamples
{
    public class NameParser
    {
        public string GetLastName(string fullName)
        {
            var indexOfLastSpace =  fullName.LastIndexOf(" ", StringComparison.CurrentCulture);
            if (indexOfLastSpace >= 0)
            {
                var span = fullName.AsSpan();
                return span.Slice(indexOfLastSpace + 1).ToString();
            }

            return string.Empty;
        }
    }
}
