using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using FunctionLibrary;

namespace SpeedTesterCLI.Benchmarks
{
    // MemoryDiagnoser(false) means that we also want to benchmarck the memory use of our functions
    // ReturnValueValidator(true) means that the benchmarks will only be run if all benchmarks return the same value
    // ArtifactsPath(@"C:\Temp\BenchmarkDotNet.Artifacts\ArrayCloning") means that the output files from the benchmark will be saved to C:\Temp\BenchmarkDotNet.Artifacts\ArrayCloning
    [MemoryDiagnoser(false), ReturnValueValidator(true), ArtifactsPath(@"C:\Temp\BenchmarkDotNet.Artifacts\ArrayCloning")]
    public class ArrayCloning
    {
        // Contains the Length of the Byte Array. Each Benchmark will be run once for each value in each Params attribute
        [Params(1, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
        public int Length;

        // Seeded Random number generator to ensure the same random values between each benchmark
        public Random Rand = new Random(12011945);

        // Contains our mock/test data
        public byte[] Bytes = null!;

        // GlobalSetup is run before each benchmark starts.
        // This is where we initialize our mock data,
        [GlobalSetup]
        public void Setup()
        {
            this.Bytes = new byte[Length];
            Rand.NextBytes(Bytes);
        }

        [Benchmark]
        public byte[] CloneArrayByLoop()
        {
            return this.Bytes.CloneArrayByLoop();
        }

        [Benchmark]
        public byte[] CloneArrayByArrayCopy()
        {
            return this.Bytes.CloneArrayByArrayCopy();
        }

        [Benchmark]
        public byte[] CloneArrayBySpanLoop()
        {
            return this.Bytes.CloneArrayBySpanLoop();
        }

        [Benchmark]
        public byte[] CloneArrayBySpanCopy()
        {
            return this.Bytes.CloneArrayBySpanCopy();
        }
    }
}
