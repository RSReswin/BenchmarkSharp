using BenchmarkDotNet.Running;
using BenchmarkSharp.TestArea;
using BenchmarkSharp.TestBenches;

BenchmarkRunner.Run<SpanBench>();
// Console.WriteLine(SpanArea.IntToCompactNo(10_00_000));
// Console.WriteLine(SpanArea.IntToCompactNo_WithReadOnlySpan(10_00_000).ToString());