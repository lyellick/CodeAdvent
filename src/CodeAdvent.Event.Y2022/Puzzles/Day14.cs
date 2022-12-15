using CodeAdvent.Common.Extensions;
using CodeAdvent.Common.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
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

            simultion.Run();

            string cave = simultion.ToString();

            Assert.That(simultion.SandGrainCount, Is.EqualTo(578));
        }

        [Test]
        public void Part2()
        {
            var layout = _puzzle
                .ToEnumerable<(int row, int col)[]>("\n", " -> ", (row) => row
                    .Select(coord => coord.Split(","))
                    .Select(coord => (int.Parse(coord[0]), int.Parse(coord[1])))
                    .ToArray())
                .ToArray();

            var simultion = new CaveSimultion(layout, (0, 500), (158, 506), true);

            simultion.Run();

            string cave = simultion.ToString();

            Assert.That(simultion.SandGrainCount, Is.EqualTo(24377));
        }
    }

    public class CaveSimultion
    {
        public long SandGrainCount { get; set; } = 0;

        private int[][] _cave;

        private (int row, int col) _start;

        private readonly bool _floor;

        public CaveSimultion((int row, int col)[][] layout, (int row, int col) start, (int width, int length) boundry, bool floor = false)
        {
            _floor = floor;

            _start = start;

            if (!_floor)
                _cave = JaggedArrayUtility.Create<int[][]>(boundry.width + 1, boundry.length * 2);
            else
                _cave = JaggedArrayUtility.Create<int[][]>(boundry.width + 10, boundry.length * 2);

            foreach (var coordinates in layout)
                for (int coordinate = 0; coordinate < coordinates.Length - 1; coordinate++)
                    foreach (var (row, col) in coordinates[coordinate].CreateLine(coordinates[coordinate + 1]))
                        _cave[row][col] = 1;

            if (_floor)
            {
                var row = layout.Max(line => line.Max(x => x.col)) + 2;

                for (int col = 0; col < _cave[row].Length; col++)
                    _cave[row][col] = 1;
            }

            _cave[start.row][start.col] = 3;
        }

        public void Run()
        {
            bool drop = true;

            do
            {
                drop = AddSandGrain();
            } while (drop);
        }

        private bool AddSandGrain()
        {
            Grain start = new(_start.row, _start.col);

            var resting = GetRestingLocation(start);

            if (resting != null)
            {
                _cave[resting.Row][resting.Col] = 2;

                SandGrainCount++;

                return true;
            }

            if (resting == null && _floor)
                SandGrainCount++;

            return false;
        }

        private Grain GetRestingLocation(Grain current)
        {
            Grain next = null;

            if (!_cave.IsWithinBounds(current.Row, current.Col))
                return null;

            var value = _cave[current.Row][current.Col];

            switch (value)
            {
                case 0:
                case 3:
                    next = GetRestingLocation(new(current.Row + 1, current.Col));
                    break;
                case 1:
                    if (_cave[current.Row][current.Col - 1] != 1 || _cave[current.Row][current.Col + 1] != 1)
                        next = GetDirection(current, next);
                    else
                        next = new(current.Row - 1, current.Col);
                    break;
                case 2:
                    next = GetDirection(current, next);
                    break;
            }

            return next;
        }

        private Grain GetDirection(Grain current, Grain next)
        {
            var left = GoLeft(current);
            var right = GoRight(current);
            if (left != null)
            {
                next = GetRestingLocation(left);
            }
            else if (right != null)
            {
                next = GetRestingLocation(right);
            }
            else if (left == null && right == null)
            {
                if (current.Row != 1)
                    next = new(current.Row - 1, current.Col);
                else
                    return null;
            }
            else
            {
                // Edge Case
            }

            return next;
        }

        private Grain GoLeft(Grain current)
        {
            Grain next = new(current.Row, current.Col - 1);

            var value = _cave[next.Row][next.Col];

            if (value == 0)
                return next;

            return null;
        }

        private Grain GoRight(Grain current)
        {
            Grain next = new(current.Row, current.Col + 1);

            var value = _cave[next.Row][next.Col];

            if (value == 0)
                return next;

            return null;
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

        public Grain(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}