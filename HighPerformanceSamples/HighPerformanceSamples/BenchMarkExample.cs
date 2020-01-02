using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace HighPerformanceSamples
{
    public class Program
    {
        public static void Main(string[] args) => _ = BenchmarkRunner.Run<ArrayBenchmarks>();
    }

    [MemoryDiagnoser]
    public class NameParserBenchMarks
    {
        private const string FullName = "Steve J Gordon";
        private static readonly NameParser Parser = new NameParser();

        [Benchmark]
        public void GetLastName()
        {
            Parser.GetLastName(FullName);
        }
    }
}
