using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace Benchmarks
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .With(Job.Default.With(Runtime.Clr).With(CsProjClassicNetToolchain.Net472))
                .With(Job.Default.With(Runtime.Core).With(CsProjCoreToolchain.NetCoreApp22))
                .With(Job.Default.With(Runtime.Core).With(CsProjCoreToolchain.NetCoreApp30))
                .With(MemoryDiagnoser.Default)
                .With(MemoryDiagnoser.Default);
            
            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args, config);
        }
    }
}
