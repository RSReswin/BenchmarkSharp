using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class ArrayPoolBench
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
    public void ArrayPool_PoolRent_InsideLoop()
    {
        var pool = ArrayPool<byte>.Shared;
        
        for (int i = 0; i < 100; i++)
        {
            byte[] buffer = pool.Rent(100);
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = 1;
            }
            
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = (byte)(buffer[i1] * 2);
            }
            pool.Return(buffer, false);
        }
    }
    
    [Benchmark]
    public void ArrayPool_PoolRent_OutSideLoop()
    {
        var pool = ArrayPool<byte>.Shared;
        byte[] buffer = pool.Rent(100);
        
        for (int i = 0; i < 100; i++)
        {
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = 1;
            }
            
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = (byte)(buffer[i1] * 2);
            }
            pool.Return(buffer, false);
        }
    }
    
    [Benchmark]
    public void ArrayPool_CleanArray_True()
    {
        var pool = ArrayPool<byte>.Shared;
        
        for (int i = 0; i < 100; i++)
        {
            byte[] buffer = pool.Rent(100);
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = 1;
            }
            
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = (byte)(buffer[i1] * 2);
            }
            pool.Return(buffer, true);
        }
    }
    
    [Benchmark]
    public void ArrayPool_CleanArray_False()
    {
        var pool = ArrayPool<byte>.Shared;
        
        for (int i = 0; i < 100; i++)
        {
            byte[] buffer = pool.Rent(100);
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = 1;
            }
            
            for (var i1 = 0; i1 < buffer.Length; i1++)
            {
                buffer[i1] = (byte)(buffer[i1] * 2);
            }
            pool.Return(buffer, false);
        }
    }
}