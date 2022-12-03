namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 3: Rucksack Reorganization
    /// </summary>
    public class Day03
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 3);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var rucksacks = _puzzle.ToEnumerable((rucksack) => rucksack.ToArray().Split(rucksack.Length / 2).ToArray()).ToArray();

            var intersects = rucksacks.Select(rucksack => rucksack[0].Intersect(rucksack[1]).First()).ToArray();

            var sum = intersects.Select(intersect => char.IsUpper(intersect) ? intersect - 38 : intersect - 96).Sum();

            Assert.That(sum, Is.EqualTo(7766));
        }

        [Test]
        public void Part2()
        {
            var rucksacks = _puzzle.ToEnumerable((rucksack) => rucksack).ToArray().GenerateSplits(3).ToArray();

            //var intersects = rucksacks.Select(groups => groups.Skip(1).Aggregate(new HashSet<char[]>(groups.First().ToArray()), (prev, next) => { prev.IntersectWith(next); return prev; }));

            Assert.Pass();
        }
    }
}