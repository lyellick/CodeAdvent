namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 10: Cathode-Ray Tube
    /// </summary>
    public class Day10
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 10);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var program = _puzzle
                .ToEnumerable<(int cycles, int value)>(
                    @"(.+) (.+)", (instruction, isMatch) => isMatch ? (2, int.Parse(instruction.Groups[2].Value)) : (1, 0))
                .ToArray();

            int pass = 1, signal = 0, x = 1;

            foreach (var (cycles, value) in program)
            {
                if (pass % 40 == 20)
                    signal += pass * x; 

                if (cycles == 2)
                {
                    pass++;

                    if (pass % 40 == 20)
                        signal += pass * x;

                    x += value;
                }

                pass++;
            }

            Assert.That(signal, Is.EqualTo(10760));
        }

        [Test]
        public void Part2()
        {
            var program = _puzzle
                .ToEnumerable<(int cycles, int value)>(
                    @"(.+) (.+)", (instruction, isMatch) => isMatch ? (2, int.Parse(instruction.Groups[2].Value)) : (1, 0))
                .ToArray();

            string display = "";

            int pass = 1, signal = 0, x = 1;

            foreach (var (cycles, value) in program)
            {
                display += Math.Abs((pass % 40) - x) <= 1 ? "▓" : "░";

                if (pass % 40 == 20)
                    signal += pass * x;
                else if (pass % 40 == 0)
                    display += "\n";
                
                if (cycles == 2)
                {
                    display += Math.Abs((pass % 40) - x) <= 1 ? "▓" : "░";

                    pass++;

                    if (pass % 40 == 20)
                        signal += pass * x;
                    else if (pass % 40 == 0)
                        display += "\n";

                    x += value;
                }

                pass++;
            }

            Assert.That(signal, Is.EqualTo(10760));
        }
    }
}