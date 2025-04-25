using BenchmarkDotNet.Attributes;

namespace BenchmarkSharp.TestBenches;

[MemoryDiagnoser]
public class StringSlice
{
    private const string DataInText = "07-11-2002";
    
    [Benchmark]
    public void Normal_SliceString()
    {
        var day = DataInText.Substring(0, 2);
        var month = DataInText.Substring(3, 2);
        var year = DataInText.Substring(6, 4);
        var nDay = Int32.Parse(day);
        var nMonth = Int32.Parse(month);
        var nYear = Int32.Parse(year);
    }
    
    [Benchmark]
    public void ReadOnlySpan_SliceString()
    {
        ReadOnlySpan<char> fullDataSpan = DataInText;
        var day = fullDataSpan.Slice(0, 2);
        var month = fullDataSpan.Slice(3, 2);
        var year = fullDataSpan.Slice(6, 4);
        var nDay = Int32.Parse(day);
        var nMonth = Int32.Parse(month);
        var nYear = Int32.Parse(year);
    }
}