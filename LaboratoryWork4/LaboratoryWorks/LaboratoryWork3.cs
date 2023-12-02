using Point = (int X, int Y);

namespace LaboratoryWorks;


public static class LaboratoryWork3
{
    public static string Run(string inputText)
    {
        var lines = inputText
            .ReplaceLineEndings("\n")
            .Split("\n")
            .AsEnumerable()
            .GetEnumerator();
        var input = Parse(lines);
        ValidateInput(input);
        var calculator = KnightNumberMovesCalculator.Instance;
        var minNumberMoves = calculator.Calculate(
            size: (input.N, input.N),
            point1: input.Point1,
            point2: input.Point2
        );
        return minNumberMoves.ToString();
    }


    private static Input Parse(IEnumerator<string> lines)
    {
        if (!lines.MoveNext())
        {
            throw new InputException("The input does not contain board size line.");
        }
        if (!int.TryParse(lines.Current, out var n))
        {
            throw new InputException("Board size must be an integer.");
        }

        if (!lines.MoveNext())
        {
            throw new InputException("The input does not contain the coordinates of the starting cell.");
        }
        var parts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (!(
            parts is [var x1Text, var y1Text]
            && int.TryParse(x1Text, out var x1)
            && int.TryParse(y1Text, out var y1)
        ))
        {
            throw new InputException("Invalid starting cell coordinates.");
        }

        if (!lines.MoveNext())
        {
            throw new InputException("The input does not contain the coordinates of the end cell.");
        }
        parts = lines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (!(
            parts is [var x2Text, var y2Text]
            && int.TryParse(x2Text, out var x2)
            && int.TryParse(y2Text, out var y2)
        ))
        {
            throw new InputException("Invalid end cell coordinates.");
        }

        return new()
        {
            N = n,
            Point1 = (x1, y1),
            Point2 = (x2, y2)
        };
    }


    private static void ValidateInput(Input input)
    {
        var message = input switch
        {
            { N: < 4 } => "The board size must be greater than or equal to 4.",

            { Point1.X: < 0 } => "The abscissa of the starting point must be greater than zero.",
            var i when i.Point1.X > i.N =>
                "The abscissa of the starting point must not be larger than the size of the board.",

            { Point1.Y: < 0 } => "The ordinate of the starting point must be greater than zero.",
            var i when i.Point1.Y > i.N =>
                "The ordinate of the starting point must not be larger than the size of the board.",

            { Point2.X: < 0 } => "The abscissa of the end point must be greater than zero.",
            var i when i.Point2.X > i.N =>
                "The abscissa of the end point must not be larger than the size of the board.",

            { Point2.Y: < 0 } => "The ordinate of the end point must be greater than zero.",
            var i when i.Point2.Y > i.N =>
                "The ordinate of the end point must not be larger than the size of the board.",

            _ => null
        };
        if (message is not null)
        {
            throw new InputException(message);
        }
    }


    private class Input
    {
        public required int N { get; init; }

        public required Point Point1 { get; init; }

        public required Point Point2 { get; init; }
    }
}