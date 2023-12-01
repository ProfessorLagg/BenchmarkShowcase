using BenchmarkDotNet.Running;

namespace SpeedTesterCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks.MyPerformanceTest>();
        }
    }
}