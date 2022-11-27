using System;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 1: Report Repair
    /// </summary>
    public class Day01
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 1);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int answer = 0, index = 0;

            var report = _puzzle.ToEnumerable<int>().ToArray();

            do
            {
                int n1 = report[index];
                int n2 = 0;

                for (int j = 0; j < report.Length; j++)
                    if (index != j && n1 + report[j] == 2020)
                        n2 = report[j];

                if (n1 + n2 == 2020)
                    answer = n1 * n2;   

                index++;
            } while (answer == 0);

            Assert.That(answer, Is.EqualTo(181044));
        }

        [Test]
        public void Part2()
        {
            int answer = 0, index = 0;

            var report = _puzzle.ToEnumerable<int>().ToArray();

            do
            {
                int n1 = report[index];
                int n2 = 0;
                int n3 = 0;

                for (int j = 0; j < report.Length; j++)
                    for (int p = 0; p < report.Length; p++)
                        if (index != j && index != p && j != p && n1 + report[j] + report[p] == 2020)
                        {
                            n2 = report[j];
                            n3 = report[p];
                        }

                if (n1 + n2 + n3 == 2020)
                    answer = n1 * n2 * n3;

                index++;
            } while (answer == 0);

            Assert.That(answer, Is.EqualTo(82660352));
        }
    }
}