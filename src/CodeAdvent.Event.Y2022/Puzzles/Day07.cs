using CodeAdvent.Common.Extensions;
using Newtonsoft.Json;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 7: No Space Left On Device
    /// </summary>
    public class Day07
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 7);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var history = _puzzle.ToEnumerable((line) => line.Split(" ")).Skip(1).ToArray();

            VirtualDirectory vdir = new VirtualDirectory("/").MapHistory(history);

            var directories = vdir.Root.Crawl().Where(dir => dir.size <= 100000);

            int sum = directories.Sum(dir => dir.size);

            Assert.That(sum, Is.EqualTo(1642503));
        }

        [Test]
        public void Part2()
        {
            var history = _puzzle.ToEnumerable((line) => line.Split(" ")).Skip(1).ToArray();

            VirtualDirectory vdir = new VirtualDirectory("/").MapHistory(history);

            var target = 70000000 - 30000000;

            var directories = vdir.Root.Crawl().OrderBy(dir => dir.size);

            var remove = vdir.Root.GetNodeSize() - target;

            var size = directories.First(dir => dir.size >= remove).size;

            Assert.That(size, Is.EqualTo(6999588));
        }
    }
}