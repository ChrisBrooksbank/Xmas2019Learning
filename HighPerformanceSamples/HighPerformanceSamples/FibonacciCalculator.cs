using System;

namespace HighPerformanceSamples
{
    public class FibonacciCalculator
    {
        public void CalculateFibonacci()
        {
            const int arraySize = 20;
            Span<int> fib = stackalloc int[arraySize];

            // warning stack has limited capacity, not more than a few thousand
            // Span here means we dont have to resort to unsafe code to work with stack

            fib[0] = fib[1] = 1;

            for (int i=2;i < arraySize;++i)
            {
                fib[i] = fib[i-1] + fib[i-2];
            }

            // cant return the stack allocated memory
        }
    }
}
