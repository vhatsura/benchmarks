using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Algorithms
{
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
