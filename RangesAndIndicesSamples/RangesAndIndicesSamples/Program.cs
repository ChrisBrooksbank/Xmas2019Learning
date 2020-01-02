using System;
using System.Linq;

namespace RangesAndIndicesSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 10).ToArray();
            var copy = numbers[..];
        }
    }
}
