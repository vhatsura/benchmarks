using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Algorithms
{
    //BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.172 (2004/?/20H1)
    //Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
    //[Host] : .NET Framework 4.8 (4.8.4084.0), X64 RyuJIT

    //Runtime=.NET Core 3.1  Toolchain=InProcessEmitToolchain  
    //
    //|                        Method | ElementsInDictionary | ElementsInFilter |        Mean |     Error |    StdDev | Completed Work Items | Lock Contentions |  Gen 0 | Gen 1 | Gen 2 | Allocated |
    //|------------------------------ |--------------------- |----------------- |------------:|----------:|----------:|---------------------:|-----------------:|-------:|------:|------:|----------:|
    //|           IterateByDictionary |               200000 |             1000 | 9,527.84 us | 61.700 us | 51.522 us |                    - |                - |      - |     - |     - |     384 B |
    //| IterateByDictionaryInParallel |               200000 |             1000 | 6,171.44 us | 42.108 us | 37.328 us |                    - |                - |      - |     - |     - |   27520 B |
    //|                  IterateBySet |               200000 |             1000 |    42.99 us |  0.285 us |  0.252 us |                    - |                - | 0.0610 |     - |     - |     329 B |
    //|        IterateBySetInParallel |               200000 |             1000 |   140.65 us |  2.652 us |  2.838 us |                    - |                - | 2.4414 |     - |     - |   10917 B |
    //|           IterateByDictionary |               200000 |             1500 | 9,642.20 us | 49.310 us | 46.124 us |                    - |                - |      - |     - |     - |     384 B |
    //| IterateByDictionaryInParallel |               200000 |             1500 | 6,223.22 us | 57.819 us | 51.255 us |                    - |                - |      - |     - |     - |   27520 B |
    //|                  IterateBySet |               200000 |             1500 |    69.18 us |  0.623 us |  0.583 us |                    - |                - |      - |     - |     - |     329 B |
    //|        IterateBySetInParallel |               200000 |             1500 |   156.11 us |  2.378 us |  2.224 us |                    - |                - | 2.4414 |     - |     - |   10987 B |

    public class DictionaryAndHashSetBenchmark
    {
        public IDictionary<string, int> _dictionary;
        public ISet<string> _filter;

        [Params( /*10_000,*/ 200_000 /*, 1_000_000*/)]
        public int ElementsInDictionary;

        [Params( /*250,*/ 1_000, 1_500)]
        public int ElementsInFilter;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();

            _dictionary = new Dictionary<string, int>();
            do
            {
                var value = random.Next(int.MinValue, int.MaxValue);
                if (!_dictionary.ContainsKey(value.ToString()))
                {
                    _dictionary.Add(value.ToString(), value);
                }
            } while (_dictionary.Count != ElementsInDictionary);

            _filter = new HashSet<string>(ElementsInFilter);

            do
            {
                _filter.Add(random.Next(int.MinValue, int.MaxValue).ToString());
            } while (_filter.Count != ElementsInFilter);
        }

        [Benchmark]
        public IList<int> IterateByDictionary() =>
            _dictionary.Where(x => _filter.Contains(x.Key)).Select(x => x.Value).ToList();

        [Benchmark]
        public IList<int> IterateByDictionaryInParallel() =>
            _dictionary.AsParallel().Where(x => _filter.Contains(x.Key)).Select(x => x.Value).ToList();


        [Benchmark]
        public IList<int> IterateBySet() =>
            _filter.Where(x => _dictionary.ContainsKey(x)).Select(x => _dictionary[x]).ToList();

        [Benchmark]
        public IList<int> IterateBySetInParallel() =>
            _filter.AsParallel().Where(x => _dictionary.ContainsKey(x)).Select(x => _dictionary[x]).ToList();
    }
}
