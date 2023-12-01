using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace SpeedTesterCLI.Benchmarks
{
    [
        MemoryDiagnoser(false), // Include Memory stats in benchmark
        ReturnValueValidator(true), // Validate that all benchmarks return the same value
        ArtifactsPath(@"C:\Temp\BenchmarkDotNet.Artifacts\ArrayCloning"), // Save output files to C:\Temp\BenchmarkDotNet.Artifacts\ArrayCloning
        XmlExporter("", true, false) // Also export an XML file with the results in
    ]
    public class MyPerformanceTest
    {
        // Contains the Length of the Byte Array. Each Benchmark will be run once for each value in each Params attribute
        [Params(1, 10, 100, 1_000)]
        public int Length;

        // Seeded Random number generator to ensure the same random values between each benchmark
        public Random Rand = new Random(12011945);

        // Contains our mock/test data
        public double[] Numbers = null!;

        // GlobalSetup is run before each benchmark starts.
        // This is where we initialize our mock data,
        [GlobalSetup]
        public void Setup()
        {
            this.Numbers = Enumerable
                .Range(0, Length)
                .Select(i => Rand.NextDouble())
                .ToArray();
        }

        [Benchmark]
        public double Linq()
        {
            return this.Numbers.Sum();
        }

        [Benchmark]
        public double Loop()
        {
            double sum = 0.0;
            foreach (var num in this.Numbers) { sum += num; }
            return sum;
        }

        [Benchmark]
        public double LoopSpan()
        {
            double sum = 0.0;
            foreach (var num in this.Numbers.AsSpan()) { sum += num; }
            return sum;
        }
    }
}
