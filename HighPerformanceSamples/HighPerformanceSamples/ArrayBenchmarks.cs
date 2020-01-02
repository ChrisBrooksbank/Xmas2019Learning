using System;
using System.Collections;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace HighPerformanceSamples
{
    [MemoryDiagnoser]
    public class ArrayBenchmarks
    {
        private int[] _myArray;
        private static readonly Consumer Consumer = new Consumer();

        [Params(10,1000,10000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _myArray = new int[Size];

            for (var i = 0; i < Size; i++)
                _myArray[i] = i;
        }

        [Benchmark(Baseline = true)]
        public void Original() =>
            _myArray.Skip(Size / 2).Take(Size / 4).Consume(Consumer);

        [Benchmark]
        public int[] ArrayCopy()
        {
            var newArray = new int[Size / 4];
            Array.Copy(_myArray, Size / 2, newArray, 0, Size / 4);
            return newArray;
        }

        [Benchmark]
        public Span<int> Span() => _myArray.AsSpan().Slice(Size / 2, Size / 4);

    }
}
