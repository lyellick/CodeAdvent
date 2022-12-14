using System.Diagnostics;
using System.Drawing;

namespace CodeAdvent.Common.Extensions
{
    public static class TupleExtension
    {
        public static IList<(int row, int col)> CreateLine(this (int row, int col) from, (int row, int col) to)
        {
            List<(int row, int col)> points = new();

            if (from.col == to.col)
            {
                if (from.row > to.row)
                    points.AddRange(Enumerable.Range(to.row, from.row - to.row + 1).Select(y => (from.col, y)));

                if (from.row < to.row)
                    points.AddRange(Enumerable.Range(from.row, to.row - from.row + 1).Select(y => (from.col, y)));
            }

            // move left or right
            if (from.row == to.row)
            {
                if (from.col > to.col)
                    points.AddRange(Enumerable.Range(to.col, from.col - to.col + 1).Select(x => (x, from.row)));

                if (from.col < to.col)
                    points.AddRange(Enumerable.Range(from.col, to.col - from.col + 1).Select(x => (x, from.row)));
            }

            return points;
        }
    }
}
