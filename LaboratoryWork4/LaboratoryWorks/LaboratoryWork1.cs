using System.Collections.Frozen;

namespace LaboratoryWorks;


public static class LaboratoryWork1
{
    private static readonly FrozenDictionary<char, IEnumerable<char>> _transformations;


    static LaboratoryWork1()
    {
        var transformations = new Dictionary<char, IEnumerable<char>>();
        for (var symbol = '0'; symbol <= '9'; ++symbol)
        {
            transformations[symbol] = symbol.ToString();
        }
        for (var symbol = 'a'; symbol <= 'g'; ++symbol)
        {
            var transformationNumbers = Enumerable.Range(symbol - 'a', 4);
            transformations[symbol] = string.Join("", transformationNumbers);
        }
        transformations['?'] = string.Join("", Enumerable.Range(0, 10));
        _transformations = transformations.ToFrozenDictionary();
    }


    public static string Run(string inputText)
    {
        var lines = inputText
            .ReplaceLineEndings("\n")
            .Split("\n")
            .AsEnumerable()
            .GetEnumerator();
        var input = Parse(lines);
        ValidateInput(input, _transformations);
        var numberIntersections = CalculateNumberIntersections(
            input.FirstPattern,
            input.SecondPattern,
            _transformations
        );
        return numberIntersections.ToString();
    }


    private static Input Parse(IEnumerator<string> lines)
    {
        if (!lines.MoveNext())
        {
            throw new InputException("The input does not contain first pattern.");
        }
        var firstPattern = lines.Current;

        if (!lines.MoveNext())
        {
            throw new InputException("The input does not contain second pattern.");
        }
        var secondPattern = lines.Current;

        return new()
        {
            FirstPattern = firstPattern,
            SecondPattern = secondPattern
        };
    }


    private static void ValidateInput(
        Input input,
        IReadOnlyDictionary<char, IEnumerable<char>> transformations
    )
    {
        if (input.FirstPattern.Length != input.SecondPattern.Length)
        {
            throw new InputException("The length of the patterns must be the same.");
        }
        var size = input.FirstPattern.Length;

        for (int i = 0; i < size; ++i)
        {
            var firstSymbol = input.FirstPattern[i];
            if (!transformations.ContainsKey(firstSymbol))
            {
                throw new InputException(
                    "The first pattern contains an invalid character" +
                    $"'{firstSymbol}' at {i + 1}."
                );
            }
            var secondSymbol = input.SecondPattern[i];
            if (!transformations.ContainsKey(secondSymbol))
            {
                throw new InputException(
                    "The second pattern contains an invalid character" +
                    $"'{secondSymbol}' at {i + 1}."
                );
            }
        }
    }


    private static int CalculateNumberIntersections(
        string firstPattern,
        string secondPattern,
        IReadOnlyDictionary<char, IEnumerable<char>> transformations
    )
    {
        var size = firstPattern.Length;
        var result = 1;
        for (int i = 0; i < size; ++i)
        {
            var firstTransformation = transformations[firstPattern[i]];
            var secondTransformation = transformations[secondPattern[i]];
            result *= firstTransformation.Intersect(secondTransformation).Count();
        }
        return result;
    }


    private class Input
    {
        public required string FirstPattern { get; init; }

        public required string SecondPattern { get; init; }
    }
}