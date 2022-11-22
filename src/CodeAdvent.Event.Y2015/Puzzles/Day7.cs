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

        private (string gate, bool solved, string wire, int? signal)[] ProcessInstructions(string input)
        {
            var circuits = MapInstructions(_input).ToArray();

            do
            {
                var completed = circuits.Where(circuit => circuit.solved).ToArray();
                
                var remaining = circuits.Where(circuit => !circuit.solved).ToArray();

                for (int i = 0; i < remaining.Length; i++)
                {
                    foreach (var complete in completed)
                        if (TrySafeReplace(remaining[i].gate, complete.wire, $"{complete.signal}", out string gate))
                            remaining[i].gate = gate;

                    string[] parts = remaining[i].gate.Split(' ');

                    switch (parts.Length)
                    {
                        case 2:
                            if (int.TryParse(parts[1], out int not))
                            {
                                string operation = parts[0];

                                switch (operation)
                                {
                                    case "NOT":
                                        remaining[i].signal = (ushort)~not;
                                        remaining[i].solved = true;
                                        break;
                                }
                            }
                            break;
                        case 3:
                            if (int.TryParse(parts[0], out int left) && int.TryParse(parts[2], out int right))
                            {
                                string operation = parts[1];

                                switch (operation)
                                {
                                    case "AND":
                                        remaining[i].signal = left & right;
                                        remaining[i].solved = true;
                                        break;
                                    case "OR":
                                        remaining[i].signal = left | right;
                                        remaining[i].solved = true;
                                        break;
                                    case "LSHIFT":
                                        remaining[i].signal = left << right;
                                        remaining[i].solved = true;
                                        break;
                                    case "RSHIFT":
                                        remaining[i].signal = left >> right;
                                        remaining[i].solved = true;
                                        break;
                                }

                                remaining[i].solved = true;
                            }
                            break;
                    }
                }

                circuits = completed.Concat(remaining).OrderBy(circuit => circuit.wire).ToArray();

            } while (!circuits.All(circuit => circuit.solved));

            return circuits;
        }

        private IEnumerable<(string gate, bool solved, string wire, int? signal)> MapInstructions(string input)
        {
            using var reader = new StringReader(input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                string[] parts = line.Split(" -> ");

                yield return int.TryParse(parts[0], out int signal)
                    ? (parts[0], true, parts[1], signal)
                    : (parts[0], false, parts[1], null);
            }
        }

        private bool TrySafeReplace(string input, string find, string replace, out string output)
        {
            Regex search = new($@"\b{find}\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            output = null;

            if (search.IsMatch(input))
                output = search.Replace(input, replace);

            return search.IsMatch(input);
        }
    }
}