using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class UnSafeBench
{
    private const int DataSize = 1000000;
    private readonly int[] _data1 = new int[DataSize];
    private readonly int[] _data2 = new int[10];

    [Benchmark]
    public void Normal_ModifyData()
    {
        for (int i = 0; i < _data1.Length; i++)
        {
            _data1[i] = i;
        }
    }

    [Benchmark]
    public unsafe void Unsafe_ModifyData()
    {
        fixed (int* ptr = _data1)
        {
            for (int i = 0; i < DataSize; i++)
            {
                ptr[i] = i;
            }
        }
    }

    [Benchmark]
    public void Normal_Access()
    {
        _data2[0] = 0;
        _data2[1] = 1;
        _data2[2] = 2;
        _data2[3] = 3;
        _data2[4] = 4;
        _data2[5] = 5;
        _data2[6] = 6;
        _data2[7] = 7;
        _data2[8] = 8;
        _data2[9] = 9;
    }

    [Benchmark]
    public unsafe void Unsafe_Access()
    {
        fixed (int* ptr = _data2)
        {
            ptr[0] = 0;
            ptr[1] = 1;
            ptr[2] = 2;
            ptr[3] = 3;
            ptr[4] = 4;
            ptr[5] = 5;
            ptr[6] = 6;
            ptr[7] = 7;
            ptr[8] = 8;
            ptr[9] = 9;
        }
    }
}