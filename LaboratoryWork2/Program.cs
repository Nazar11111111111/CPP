const string
    InputPath = "INPUT.TXT",
    OutputPath = "OUTPUT.TXT";


var text = File.ReadAllText(InputPath);
int n;
try
{
    n = Parse(text);
    ValidateInput(n);
}
catch (InputException exception)
{
    File.WriteAllText(OutputPath, exception.Message);
    return;
}
var multipliers = new long[] { 2L, 3L, 5L };
var sequence = CalculateSequence(n, multipliers);
File.WriteAllText(OutputPath, sequence[^1].ToString());


static int Parse(string text)
{
    if (int.TryParse(text, out var n))
    {
        return n;
    }
    throw new InputException("The input must contain an integer.");
}


static void ValidateInput(int n)
{
    if (n <= 0)
    {
        throw new InputException("The number 'n' must be a natural number.");
    }
}


static long[] CalculateSequence(int n, IEnumerable<long> multipliers, long start = 1L)
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


file class InputException(string message) : Exception(message);