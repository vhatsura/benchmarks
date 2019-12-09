using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Environments;

namespace Benchmarks.HashCode
{
    public enum Type
    {
        Open = 1,
        Close = 2
    }
    
    public class ResharperVsHashCodeStruct
    {
        public int _id;
        public string _name;
        public Type _type;
        
        [GlobalSetup]
        public void Setup()
        {
            var rnd = new Random();
            _id = rnd.Next();
            _type = (Type) rnd.Next(1, 2);
            _name = new string(Enumerable.Range(0, rnd.Next(1_000)).Select(x => (char)rnd.Next(0, 33)).ToArray());
        }

        [Benchmark]
        public int ResharperHashCode()
        {
            unchecked
            {
                var hashCode = _id;
                hashCode = (hashCode * 397) ^ _name.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) _type;
                return hashCode;
            }
        }

        [Benchmark]
        public int HashCodeStruct()
        {
            return System.HashCode.Combine(_id, _name, _type);
        }
    }
}
