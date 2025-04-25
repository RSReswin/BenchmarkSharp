using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class GarbageCollectorBench
{
    private class Garbage
    {
        private long _number1 = Random.Shared.Next(10_000, 100_000);
        private long _number2 = Random.Shared.Next(10_000, 100_000);
        private long _number3 = Random.Shared.Next(10_000, 100_000);
        private long _number4 = Random.Shared.Next(10_000, 100_000);
        private long _number5 = Random.Shared.Next(10_000, 100_000);
    }

    [Benchmark]
    public void GC_NotClean()
    {
        for (int i = 0; i < 100; i++)
        {
            var newObject = new Garbage();
        }
    }

    [Benchmark]
    public void GC_Clean_AfterLoopCompleted()
    {
        for (int i = 0; i < 100; i++)
        {
            var newObject = new Garbage();
        }
        
        GC.Collect(0, GCCollectionMode.Aggressive, true);
        GC.Collect(1, GCCollectionMode.Aggressive, true);
        GC.Collect(2, GCCollectionMode.Aggressive, true);
    }
}