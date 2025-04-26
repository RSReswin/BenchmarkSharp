using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class ScopedKeywordBench
{
    [Benchmark]
    public void Normal_ModifyData()
    {
        Span<int> span = stackalloc int[100];
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = i;
        }
    }
    
    [Benchmark]
    public void Scoped_ModifyData()
    {
        scoped Span<int> span = stackalloc int[100];
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = i;
        }
    }
}