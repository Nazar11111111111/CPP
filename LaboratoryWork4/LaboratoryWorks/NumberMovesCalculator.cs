using Point = (int X, int Y);

namespace LaboratoryWorks;


internal abstract class NumberMovesCalculator
{
    public int Calculate(Point size, Point point1, Point point2)
    {
        const int
            UnreachableNumberMoves = -1,
            NotVisitedNumberMoves = -2,
            InVisitingNumberMoves = -3;


        var numberMovesArray = new int[size.X, size.Y];
        for (var i = 0; i < size.X; ++i)
        {
            for (var j = 0; j < size.Y; ++j)
            {
                numberMovesArray[i, j] = NotVisitedNumberMoves;
            }
        }
        return Calculate(point1);


        int Calculate(Point point)
        {
            if (point.X < 0 || point.Y < 0 || point.X >= size.X || point.Y >= size.Y)
            {
                return UnreachableNumberMoves;
            }
            if (point == point2)
            {
                return 0;
            }
            ref var result = ref numberMovesArray[point.X, point.Y];
            if (result != NotVisitedNumberMoves)
            {
                return result;
            }

            result = InVisitingNumberMoves;
            var nextNumberMovesValues = GetMoves(size, point)
                .Select(Calculate)
                .Where(n => n != UnreachableNumberMoves && n != InVisitingNumberMoves);
            return result = nextNumberMovesValues.Any()
                ? nextNumberMovesValues.Min() + 1
                : UnreachableNumberMoves;
        }
    }


    protected abstract IEnumerable<Point> GetMoves(Point size, Point point);
}