using CodeAdvent.Common.Extensions;
using CodeAdvent.Common.Models;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 14: Regolith Reservoir
    /// </summary>
    public class Day14
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 14);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var layout = _puzzle
                .ToEnumerable<(int row, int col)[]>("\n", " -> ", (row) => row
                    .Select(coord => coord.Split(","))
                    .Select(coord => (int.Parse(coord[0]), int.Parse(coord[1])))
                    .ToArray())
                .ToArray();

            var simultion = new CaveSimultion(layout, (0, 500), (158, 506));

            bool drop = true;

            do
            {
                simultion.ProcessGrain();

                string cave = simultion.ToString();
            } while (drop);

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }

    public class CaveSimultion
    {
        private int[][] _cave;

        private (int row, int col) _start;

        public CaveSimultion((int row, int col)[][] layout, (int row, int col) start, (int width, int length) boundry)
        {
            _start = start;

            _cave = JaggedArrayUtility.Create<int[][]>(boundry.width + 1, boundry.length * 2);

            foreach (var coordinates in layout)
                for (int coordinate = 0; coordinate < coordinates.Length - 1; coordinate++)
                    foreach (var (row, col) in coordinates[coordinate].CreateLine(coordinates[coordinate + 1]))
                        _cave[row][col] = 1;

            _cave[start.row][start.col] = 3;
        }

        public void ProcessGrain()
        {
            Grain grain = new(_start);

        }

        public override string ToString()
        {
            string output = "";

            Dictionary<int, string> map = new()
            {
                { 0, "."},
                { 1, "#"},
                { 2, "o"},
                { 3, "+"}
            };

            for (int row = 0; row < _cave.Length; row++)
            {
                string line = "";

                for (int col = 0; col < _cave[row].Length; col++)
                    line += map[_cave[row][col]];

                output += line + "\n";
            }

            return output;
        }
    }

    public class Grain
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public virtual Grain Previous { get; set; }

        public Grain((int row, int col) start, Grain previous = null)
        {
            Row = start.row;
            Col = start.col;
            Previous = previous;
        }
    }

    public static class CaveExtension
    {
        public static int GetValue(this int[][] cave, Grain sand) => cave[sand.Row][sand.Col];
    }
}