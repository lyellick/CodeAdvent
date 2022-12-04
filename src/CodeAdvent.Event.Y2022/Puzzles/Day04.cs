using CodeAdvent.Common.Extensions;

namespace CodeAdvent.Event.Y2022.Puzzles
{
    /// <summary>
    /// Day 4: Camp Cleanup
    /// </summary>
    public class Day04
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2022, 4);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var assignments = _puzzle.ToEnumerable<(int[] first, int[] second)>(@"(\d+)-(\d+),(\d+)-(\d+)", (match) => 
            {
                (int start, int end) first = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                (int start, int end) second = (int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                if (first.start == first.end && second.start != second.end)
                    return (new int[] { first.start }, Enumerable.Range(second.start, second.end - second.start + 1).ToArray());

                if (first.start != first.end && second.start == second.end)
                    return (Enumerable.Range(first.start, first.end - first.start + 1).ToArray(), new int[] { second.start });

                return (Enumerable.Range(first.start, first.end - first.start + 1).ToArray(), Enumerable.Range(second.start, second.end - second.start + 1).ToArray());
            }).ToArray();

            var intersects = assignments.Select(assignment => assignment.first.Intersect(assignment.second).Count() == assignment.first.Length || assignment.first.Intersect(assignment.second).Count() == assignment.second.Length);

            var overlap = intersects.Count(group => group);

            Assert.That(overlap, Is.EqualTo(509));
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }
    }
}