using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace Benchmarks;

internal static class Program
{
    public static void Main(string[] args)
    {
        var config = DefaultConfig.Instance
            .AddJob(Job.Default.WithRuntime(CoreRuntime.Core60).WithToolchain(InProcessEmitToolchain.Instance))
            //.AddJob(Job.Default.WithRuntime(CoreRuntime.Core70).WithToolchain(InProcessEmitToolchain.Instance))
            //.AddDiagnoser(ThreadingDiagnoser.Default)
            .AddDiagnoser(MemoryDiagnoser.Default);

        BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args, config);
    }
}
