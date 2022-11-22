using System.Linq;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 7: Some Assembly Required
    /// </summary>
    public class Day7
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 7);
        }

        [Test]
        public void Part1()
        {
            int signal = ProcessInstructions(_input).First(circut => circut.wire.Equals("a")).signal.Value;

            Assert.That(signal, Is.EqualTo(3176));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (string gate, string wire, int? signal)[] ProcessInstructions(string input)
        {
            var circuits = MapInstructions(_input).ToArray();

            do
            {
                for (int i = 0; i < circuits.Length; i++)
                {
                    foreach (var complete in circuits)
                        if (TrySafeReplace(circuits[i].gate, complete.wire, complete.signal, out string gate))
                        {
                            circuits[i].gate = gate;

                            if (int.TryParse(gate, out int signal))
                                circuits[i].signal = signal;
                        }

                    string[] parts = circuits[i].gate.Split(' ');

                    switch (parts.Length)
                    {
                        case 2:
                            if (int.TryParse(parts[1], out int not))
                            {
                                string operation = parts[0];

                                switch (operation)
                                {
                                    case "NOT": circuits[i].signal = (ushort)~not; break;
                                }
                            }
                            break;
                        case 3:
                            if (int.TryParse(parts[0], out int left) && int.TryParse(parts[2], out int right))
                            {
                                string operation = parts[1];

                                switch (operation)
                                {
                                    case "AND": circuits[i].signal = left & right; break;
                                    case "OR": circuits[i].signal = left | right; break;
                                    case "LSHIFT": circuits[i].signal = left << right; break;
                                    case "RSHIFT": circuits[i].signal = left >> right; break;
                                }
                            }
                            break;
                    }
                }

                circuits = circuits.OrderBy(circuit => circuit.wire).ToArray();

            } while (!circuits.All(circuit => circuit.signal.HasValue));

            return circuits;
        }

        private IEnumerable<(string gate, string wire, int? signal)> MapInstructions(string input)
        {
            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                string[] parts = line.Split(" -> ");

                yield return int.TryParse(parts[0], out int signal)
                    ? (parts[0], parts[1], signal)
                    : (parts[0], parts[1], null);
            }
        }

        private bool TrySafeReplace(string input, string find, int? replace, out string output)
        {
            output = null;

            if (!replace.HasValue)
                return false;

            Regex search = new($@"\b{find}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (search.IsMatch(input))
                output = search.Replace(input, $"{replace}");

            return search.IsMatch(input);
        }
    }
}