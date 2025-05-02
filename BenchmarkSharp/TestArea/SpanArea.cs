namespace BenchmarkSharp.TestArea;

public static class SpanArea
{
    public static string IntToCompactNo(int value)
    {
        if (value < 1000) return value.ToString();
        
        char groupingChar = value switch
        {
            >= 1_000 and <= 9_99_999 => 'k',           
            >= 10_00_000 and <= 99_99_99_999 => 'M', 
            _ => '\0'                                
        };

        string timedNumber = value switch
        {
            >= 1_000 and <= 9_999 or >= 10_00_000 and <= 99_99_999 => value.ToString().Substring(0, 1),
            >= 10_000 and <= 99_999 or >= 1_00_00_000 and <= 99_99_99_999 => value.ToString().Substring(0, 2),
            >= 1_00_000 and <= 9_99_999 => value.ToString().Substring(0, 3),
            _ => string.Empty
        };
        
        return timedNumber + groupingChar;
    }
    
    public static string IntToCompactNo_WithSpan(int value)
    {
        if (value < 1000) return value.ToString();

        Span<char> fullDigits = stackalloc char[10];
        value.TryFormat(fullDigits, out _);

        Span<char> buffer = stackalloc char[4]; // e.g., "999k"

        Int16 digits = value switch
        {
            < 10_000 => 1,         // 1k to 9k
            < 100_000 => 2,        // 10k to 99k
            < 1_000_000 => 3,      // 100k to 999k
            < 10_000_000 => 1,     // 1M to 9M
            < 100_000_000 => 2,    // 10M to 99M
            _ => 3
        };

        // Copy digits
        fullDigits.Slice(0, digits).CopyTo(buffer);

        // Add suffix
        buffer[digits] = value switch
        {
            < 1_000_000 => 'k',
            _ => 'M'
        };

        // Return as string
        return new string(buffer.Slice(0, digits + 1));
    }
}