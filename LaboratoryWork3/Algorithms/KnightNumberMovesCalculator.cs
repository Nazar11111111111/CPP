using System.Collections.Immutable;
using Point = (int X, int Y);

namespace Algorithms;
using static Statics;


public class KnightNumberMovesCalculator : NumberMovesCalculator
{
    private KnightNumberMovesCalculator() { }


    public static KnightNumberMovesCalculator Instance { get; } = new();


    protected override IEnumerable<Point> GetMoves(Point size, Point point) =>
        KnightMoveOffsets.Select(o => (point.X + o.X, point.Y + o.Y));
}


file static class Statics
{
    public static ImmutableArray<Point> KnightMoveOffsets { get; } =
    [
        (2, 1),
        (1, 2),
        (-1, 2),
        (-2, 1),
        (-2, -1),
        (-1, -2),
        (1, -2),
        (2, -1)
    ];
}