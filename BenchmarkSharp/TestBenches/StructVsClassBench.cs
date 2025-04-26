using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class StructVsClassBench
{
    private const int ObjectCount = 100_000;
    
    private class MyClass
    {
        public int X;
        public int Y;
    }
    
    private struct MyStruct
    {
        public int X;
        public int Y;
    }

    [Benchmark]
    public void Allocate_Class_Array()
    {
        var arr = new MyClass[ObjectCount];
        for (int i = 0; i < ObjectCount; i++)
        {
            arr[i] = new MyClass { X = i, Y = i };
        }
    }

    [Benchmark]
    public void Allocate_Struct_Array()
    {
        var arr = new MyStruct[ObjectCount];
        for (int i = 0; i < ObjectCount; i++)
        {
            arr[i] = new MyStruct { X = i, Y = i };
        }
    }
}