namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// </summary>
    public class Day01
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 1);

            _puzzle.Input = _puzzle.Input.Replace("\n", " ").Replace("  ", "\n").Trim();

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var elves = _puzzle.ToEnumerable((elf) => Array.ConvertAll(elf.Split(" "), calorie => int.Parse(calorie))).ToArray();

            var calories = elves.Max(elf => elf.Sum());

            Assert.That(calories, Is.EqualTo(71023));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}