using Newtonsoft.Json.Linq;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 16: Proboscidea Volcanium
    /// </summary>
    public class Day16
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 16);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            string pattern = @"Valve (.+) has flow rate=(.+); tunnels? leads? to valves? (.+)";

            var report = _puzzle
                .ToEnumerable<(string label, string rate, string[] joins)>(pattern, (valve) =>
                    (valve.Groups[1].Value, valve.Groups[2].Value, valve.Groups[3].Value.Split(", ")))
                .ToArray();

            Valve[] valves = report
                .Select(valve => new Valve(valve.label, valve.rate))
                .OrderBy(valve => valve.Label)
                .ToArray();

            Array.ForEach(valves, (valve) => valve.MapConnections(report, valves));

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }

    public class Valve
    {
        public string Label { get; set; }

        public int Rate { get; set; }

        public virtual Valve[] Connections { get; set; }

        public Valve(string label, string rate)
        {
            Label = label;
            Rate = int.Parse(rate);
        }

        public void MapConnections((string label, string rate, string[] joins)[] report, Valve[] valves)
        {
            var joins = report.First(valve => valve.label == Label).joins;

            Connections = valves
                .Where(valve => joins.Contains(valve.Label))
                .OrderBy(valve => valve.Label)
                .ToArray();
        }
    }
}