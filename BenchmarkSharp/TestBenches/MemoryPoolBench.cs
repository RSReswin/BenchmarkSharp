using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class MemoryPoolBench
{
    [Benchmark]
    public void New_Array_ForEveryLoop()
    {
        for (int i = 0; i < 100; i++)
        {
            byte[] buffer = new byte[100];
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i] = 1;
            }
            
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i] = (byte)(buffer[i1] * 2);
            }
        }
    }
    
    [Benchmark]
    public void MemoryPool_PoolRent_OutSideLoop()
    {
        var pool = MemoryPool<byte>.Shared;
        IMemoryOwner<byte> buffer = pool.Rent(100);
        Memory<byte> memory = buffer.Memory;
        var spanMemory = memory.Span; 
        
        for (int i = 0; i < 100; i++)
        {
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = 1;
            }
            
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = (byte)(spanMemory[i1] * 2);
            }
            buffer.Dispose();
        }
    }
    
    [Benchmark]
    public void MemoryPool_PoolRent_InsideLoop()
    {
        var pool = MemoryPool<byte>.Shared;
        
        for (int i = 0; i < 100; i++)
        {
            IMemoryOwner<byte> buffer = pool.Rent(100);
            Memory<byte> memory = buffer.Memory;    
            var spanMemory = memory.Span; 
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = 1;
            }
            
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = (byte)(spanMemory[i1] * 2);
            }
            buffer.Dispose();
        }
    }
}