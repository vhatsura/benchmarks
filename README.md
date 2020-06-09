# Benchmarks

## Table of contents

* [Results](#results)
  * [General](#general)
  * [ASP .NET Core related](#asp-net-core-related)
    * [`ActivatorUtilities` vs `direct constructor call`](#activatorutilities-vs-direct-constructor-call)

## Results

### General

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
