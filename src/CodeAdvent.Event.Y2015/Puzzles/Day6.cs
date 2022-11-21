using System.Reflection.PortableExecutable;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 6: Probably a Fire Hazard
    /// </summary>
    public class Day6
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 6);
        }

        [Test]
        public void Part1()
        {
            int output = ProcessInstructions(_input, 1000, 1000).Cast<int>().Sum();

            Assert.That(output, Is.EqualTo(400410));
        }

        [Test]
        public void Part2()
        {
            int output = ProcessInstructions(_input, 1000, 1000, true).Cast<int>().Sum();

            Assert.That(output, Is.EqualTo(15343601));
        }

        private int[,] ProcessInstructions(string input, int start, int end, bool processBrightness = false)
        {
            int[,] grid = GenerateGrid(start, end);

            var instructions = MapInstructions(input);

            foreach (var instruction in instructions)
            {
                for (int y = instruction.start.y; y < instruction.end.y + 1; y++)
                {
                    for (int x = instruction.start.x; x < instruction.end.x + 1; x++)
                    {
                        switch (instruction.action)
                        {
                            case Action.TurnOn: grid[x, y] = processBrightness ? grid[x, y] + 1 : 1; break;
                            case Action.TurnOff: grid[x, y] = processBrightness ? grid[x, y] != 0 ? grid[x, y] - 1 : 0 : 0; break;
                            case Action.Toggle: grid[x, y] = processBrightness ? grid[x, y] + 2 : grid[x, y] == 0 ? 1 : 0; break;
                        }
                    }
                }
            }

            return grid;
        }

        private IEnumerable<(Action action, (int x, int y) start, (int x, int y) end)> MapInstructions(string input)
        {
            Regex coordinates = new(@"(\d+),(\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                Action action = line.Contains("on") ? Action.TurnOn : line.Contains("off") ? Action.TurnOff : Action.Toggle;

                var matches = coordinates.Matches(line);

                var start = (int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value));

                var end = (int.Parse(matches[1].Groups[1].Value), int.Parse(matches[1].Groups[2].Value));

                yield return (action, start, end);
            }
        }

        private int[,] GenerateGrid(int start, int end) => new int[start, end];

        private enum Action
        {
            TurnOn,
            TurnOff,
            Toggle,
            Default
        }
    }
}