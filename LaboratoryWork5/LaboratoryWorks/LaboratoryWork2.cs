namespace LaboratoryWorks;


public static class LaboratoryWork2
{
    private static readonly long[] _multipliers = [2L, 3L, 5L];


    public static string Run(string inputText)
    {
        var n = Parse(inputText);
        ValidateInput(n);
        var sequence = CalculateSequence(n, _multipliers);
        return sequence[^1].ToString();
    }


    private static int Parse(string text)
    {
        if (int.TryParse(text, out var n))
        {
            return n;
        }
        throw new InputException("The input must contain an integer.");
    }


    private static void ValidateInput(int n)
    {
        if (n <= 0)
        {
            throw new InputException("The number 'n' must be a natural number.");
        }
    }


    private static long[] CalculateSequence(
        int n,
        IEnumerable<long> multipliers,
        long start = 1L
    )
    {
        var sequence = new long[n];
        sequence[0] = start;
        var indices = new int[multipliers.Count()];

        for (int i = 1; i < n; ++i)
        {
            var nextElements = multipliers.Select((m, j) => sequence[indices[j]] * m);
            var minNextElement = nextElements.Min();
            int j = 0;
            foreach (var nextElement in nextElements)
            {
                if (nextElement == minNextElement)
                {
                    ++indices[j];
                }
                ++j;
            }

            sequence[i] = minNextElement;
        }

        return sequence;
    }
}