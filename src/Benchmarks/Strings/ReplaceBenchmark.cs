using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace Benchmarks.Strings
{
    public class ReplaceBenchmark
    {
        public string text;

        public Regex _oneRegex;
        public Regex _multipleRegex;

        [GlobalSetup]
        public void Setup()
        {
            text = "\"support@wpxhosting.com\" <\"Terry Kyle\"@mail.wpxhosting.com>";
            _oneRegex = new Regex("\"", RegexOptions.Compiled);
            _multipleRegex = new Regex("\"|\\\\|\\s", RegexOptions.Compiled);
        }

        [Benchmark]
        public string OneReplace() => text.Replace("\"", "");

        [Benchmark]
        public string OneRegex() => _oneRegex.Replace(text, "");

        [Benchmark]
        public string MultipleReplace() => text.Replace("\"", "").Replace("\\", "").Replace(" ", "");

        [Benchmark]
        public string MultipleRegex() => _multipleRegex.Replace(text, "");
    }
}
