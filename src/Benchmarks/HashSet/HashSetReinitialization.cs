using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Bogus;

namespace Benchmarks.HashSet;

public class HashSetReinitialization
{
    [Params(50, 100, 250, 500)]
    public int N;

    private HashSet<string> _set = new();
    private string[] _newWords;

    [GlobalSetup]
    public void Setup()
    {
        var dataSet = new DataSet();
        _set.UnionWith(dataSet.Random.WordsArray(15));
        _newWords = dataSet.Random.WordsArray(N);
    }

    [Benchmark]
    public void CleanAndUnionWith()
    {
        _set.Clear();
        _set.UnionWith(_newWords);
    }

    [Benchmark]
    public void New()
    {
        _set = new HashSet<string>(_newWords);
    }
}
