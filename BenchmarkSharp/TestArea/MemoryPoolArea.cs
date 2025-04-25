using System.Buffers;

namespace BenchmarkSharp.TestArea;

public static class MemoryPoolArea
{
    public static void MemoryPool_PoolRent_OutSideLoop()
    {
        var random = new Random();
        var pool = MemoryPool<byte>.Shared;
        IMemoryOwner<byte> buffer = pool.Rent(100);
        Memory<byte> memory = buffer.Memory;
        var spanMemory = memory.Span; 
        
        for (int i = 0; i < 100; i++)
        {
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = (byte)random.Next(0, 10);
            }
            
            for (var i1 = 0; i1 < spanMemory.Length; i1++)
            {
                spanMemory[i1] = (byte)(spanMemory[i1] * 2);
            }
            
            foreach (var b in spanMemory)
            {
                Console.WriteLine(b);
            }
            buffer.Dispose();
        }
    }
}