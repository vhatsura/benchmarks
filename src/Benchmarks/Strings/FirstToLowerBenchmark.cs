using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmarks.Strings
{
    public class FirstToLowerBenchmark
    {
        public string text;

        [Params(5, 10, 20)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            var dataSet = new DataSet();
            @text = dataSet.Random.String(length: N);
        }

        [Benchmark]
        public string StringFormat() => $"{text[0].ToString().ToLowerInvariant()}{text.Substring(1)}";

        [Benchmark]
        public string CharToLowerWithSubstring() => char.ToLowerInvariant(text[0]) + text.Substring(1);

        [Benchmark]
        public string CharArray()
        {
            var charArray = text.ToCharArray();
            charArray[0] = char.ToLower(charArray[0]);
            return new string(charArray);
        }
    }
}
