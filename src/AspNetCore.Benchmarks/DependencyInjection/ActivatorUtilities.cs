using System;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Benchmarks.DependencyInjection
{
    public class ActivatorUtilities
    {
        private IServiceProvider _activatorUtilities;
        private IServiceProvider _constructor;

        [GlobalSetup]
        public void Setup()
        {
            _activatorUtilities = new ServiceCollection().AddTransient<IA, A>()
                .AddTransient<IB>(sp =>
                    Microsoft.Extensions.DependencyInjection.ActivatorUtilities.CreateInstance<B>(sp))
                .BuildServiceProvider();

            _constructor = new ServiceCollection().AddTransient<IA, A>()
                .AddTransient<IB>(sp => new B(sp.GetRequiredService<IA>())).BuildServiceProvider();
        }

        [Benchmark]
        public IB CallActivatorUtilities() => _activatorUtilities.GetRequiredService<IB>();

        [Benchmark]
        public IB CallConstructor() => _constructor.GetRequiredService<IB>();

        public interface IA
        {
        }

        public interface IB
        {
        }

        public class A : IA
        {
        }

        public class B : IB
        {
            private readonly IA _a;

            public B(IA a)
            {
                _a = a;
            }
        }
    }
}
