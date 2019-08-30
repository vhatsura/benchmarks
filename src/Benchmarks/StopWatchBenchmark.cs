using System;
using System.Diagnostics;
using System.Threading;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class StopWatchBenchmark
    {
        [Benchmark]
        public long GetTimestamp()
        {
            var start = Stopwatch.GetTimestamp();
            
            // work
            Thread.Sleep(TimeSpan.FromMilliseconds(2));
            
            var stop = Stopwatch.GetTimestamp();

            return (long)((stop - start) / (double)Stopwatch.Frequency) * 1000;
        }
        
        [Benchmark]
        public long NewStopwatch()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            // work
            Thread.Sleep(TimeSpan.FromMilliseconds(2));
            
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
