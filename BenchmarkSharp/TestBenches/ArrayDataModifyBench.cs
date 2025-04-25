using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class ArrayDataModifyBench
{
    [Benchmark]
    public void Normal_Array_Modify()
    {
        int[] data = new int[100]; // Allocate memory on the heap
        data[0] = 10;
        data[1] = 20;
        
        for (int i = 0; i < 100; i++)
        {
            data[i] = data[i] * 2;
        }
    }
    
    [Benchmark	]
    public void Span_Array_Modify()
    {
        Span<int> data = stackalloc int[100]; // Allocate memory on the stack
        data[0] = 10;
        data[1] = 20;
        
        for (int i = 0; i < 100; i++)
        {
            data[i] = data[i] * 2;
        }
    }
    
    [Benchmark	]
    public void Memory_Array_Modify()
    {
        Memory<int> data = new int[100];
        var spanData = data.Span;
        spanData[0] = 10;
        spanData[1] = 20;
        
        for (int i = 0; i < 100; i++)
        {
            spanData[i] = spanData[i] * 2;
        }
    }
    
}