using BenchmarkDotNet;

namespace Textamina.Jsonite.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new BenchmarkRunner();
            runner.Run<BenchGenericDeserialize>();
        }
    }
}
