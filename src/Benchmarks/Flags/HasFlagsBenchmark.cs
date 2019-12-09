using BenchmarkDotNet.Attributes;

namespace Benchmarks.Flags
{
    public class HasFlagsBenchmark
    {
        private FlagEnum _enum;
        
        [GlobalSetup]
        public void Setup()
        {
            _enum = FlagEnum.First | FlagEnum.Second;
        }

        [Benchmark]
        public bool HasFlagIsTrue() => _enum.HasFlag(FlagEnum.First);
        
        [Benchmark]
        public bool HasFlagIsFalse() => _enum.HasFlag(FlagEnum.Third);

        [Benchmark]
        public bool AndWithEqualIsTrue() => (_enum & FlagEnum.First) == FlagEnum.First;
        
        [Benchmark]
        public bool AndWithEqualIsFalse() => (_enum & FlagEnum.Third) == FlagEnum.Third;
    }
}
