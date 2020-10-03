# Benchmarks

## Table of contents

* [Results](#results)
  * [General](#general)
    * [`string.Replace` vs `Regex.Replace`](#stringreplace-vs-regexreplace)
  * [ASP .NET Core related](#asp-net-core-related)
    * [`ActivatorUtilities` vs `direct constructor call`](#activatorutilities-vs-direct-constructor-call)

## Results

### General

#### `string.Replace` vs `Regex.Replace`

```ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

Toolchain=InProcessEmitToolchain 

```
|          Method |        Job |       Runtime |       Mean |    Error |    StdDev |     Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |----------- |-------------- |-----------:|---------:|----------:|-----------:|-------:|------:|------:|----------:|
|      OneReplace | Job-WYDLWI |      .NET 4.8 |   236.7 ns |  3.91 ns |   3.66 ns |   236.1 ns | 0.0324 |     - |     - |     136 B |
|        OneRegex | Job-WYDLWI |      .NET 4.8 |   889.1 ns | 16.94 ns |  16.63 ns |   895.2 ns | 0.3128 |     - |     - |    1316 B |
| MultipleReplace | Job-WYDLWI |      .NET 4.8 |   465.5 ns |  9.00 ns |  21.57 ns |   457.5 ns | 0.0648 |     - |     - |     273 B |
|   MultipleRegex | Job-WYDLWI |      .NET 4.8 | 2,373.0 ns | 46.80 ns | 103.71 ns | 2,341.5 ns | 0.4120 |     - |     - |    1733 B |
|      OneReplace | Job-JKCCIP | .NET Core 3.1 |   207.7 ns |  4.29 ns |  12.51 ns |   201.0 ns | 0.0324 |     - |     - |     136 B |
|        OneRegex | Job-JKCCIP | .NET Core 3.1 |   759.8 ns | 15.04 ns |  29.69 ns |   752.0 ns | 0.3128 |     - |     - |    1316 B |
| MultipleReplace | Job-JKCCIP | .NET Core 3.1 |   448.5 ns |  4.59 ns |   3.58 ns |   450.1 ns | 0.0648 |     - |     - |     273 B |
|   MultipleRegex | Job-JKCCIP | .NET Core 3.1 | 2,250.2 ns | 15.55 ns |  12.98 ns | 2,247.9 ns | 0.4120 |     - |     - |    1733 B |
|      OneReplace | Job-GYNZKR | .NET Core 5.0 |   200.7 ns |  1.55 ns |   1.38 ns |   200.9 ns | 0.0324 |     - |     - |     136 B |
|        OneRegex | Job-GYNZKR | .NET Core 5.0 |   736.5 ns |  5.86 ns |   5.48 ns |   738.7 ns | 0.3128 |     - |     - |    1316 B |
| MultipleReplace | Job-GYNZKR | .NET Core 5.0 |   448.2 ns |  4.44 ns |   4.15 ns |   446.8 ns | 0.0648 |     - |     - |     273 B |
|   MultipleRegex | Job-GYNZKR | .NET Core 5.0 | 2,277.2 ns | 18.48 ns |  17.29 ns | 2,273.7 ns | 0.4120 |     - |     - |    1733 B |


### ASP .NET Core related

#### `ActivatorUtilities` vs `direct constructor call`

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.264 (2004/?/20H1)
Intel Core i7-7700 CPU 3.60GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.201
  [Host] : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT

Toolchain=InProcessEmitToolchain  

```
|                 Method |        Job |       Runtime |     Mean |   Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |----------- |-------------- |---------:|--------:|---------:|-------:|------:|------:|----------:|
| CallActivatorUtilities | Job-IZCKJO | .NET Core 2.1 | 511.2 ns | 9.74 ns | 17.82 ns | 0.0496 |     - |     - |     208 B |
|        CallConstructor | Job-IZCKJO | .NET Core 2.1 | 114.7 ns | 2.29 ns |  3.21 ns | 0.0113 |     - |     - |      48 B |
| CallActivatorUtilities | Job-BMUTYB | .NET Core 3.1 | 497.5 ns | 9.83 ns | 20.95 ns | 0.0486 |     - |     - |     208 B |
|        CallConstructor | Job-BMUTYB | .NET Core 3.1 | 111.2 ns | 0.67 ns |  0.63 ns | 0.0113 |     - |     - |      48 B |
