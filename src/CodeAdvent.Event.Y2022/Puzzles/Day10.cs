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

            int pass = 0, signal = 0, x = 1;

            foreach (var instruction in program)
            {
                pass++;

                if (pass % 40 == 20)
                    signal += pass * x; 

                if (instruction.cycles == 2)
                {
                    pass++;

                    if (pass % 40 == 20)
                        signal += pass * x;

                    x += instruction.value;
                }
            }

            Assert.That(signal, Is.EqualTo(10760));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}