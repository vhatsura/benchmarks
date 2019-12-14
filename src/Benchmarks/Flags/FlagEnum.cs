using System;

namespace Benchmarks.Flags
{
    [Flags]
    public enum FlagEnum
    {
        First  = 0b_0000_0001,
        Second = 0b_0000_0010,
        Third  = 0b_0000_0100
    }
}