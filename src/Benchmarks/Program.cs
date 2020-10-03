using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Benchmarks
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithToolchain(InProcessEmitToolchain.Instance))
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31).WithToolchain(InProcessEmitToolchain.Instance))
                .AddJob(Job.Default.WithRuntime(CoreRuntime.Core50).WithToolchain(InProcessEmitToolchain.Instance))
                // ThreadingDiagnoser is available only in .Net Core 3.0+
                //.AddDiagnoser(ThreadingDiagnoser.Default)
                .AddDiagnoser(MemoryDiagnoser.Default);

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}
