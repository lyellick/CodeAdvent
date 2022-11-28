using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// </summary>
    public class Day05
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 5);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int[] ids = ProcessSeats(_puzzle, 128, 8);

            int highest = ids.Max();

            Assert.That(highest, Is.EqualTo(832));
        }

        [Test]
        public void Part2()
        {
            int[] ids = ProcessSeats(_puzzle, 128, 8).OrderBy(id => id).ToArray();

            int[] missing = Enumerable.Range(ids[0], ids[^1]).Except(ids).OrderBy(id => id).ToArray();

            int id = missing.First();

            Assert.That(id, Is.EqualTo(517));
        }

        private int[] ProcessSeats(CodeAdventPuzzle puzzle, int rows, int cols)
        {
            var ids = new List<int>();

            Regex pattern = new(@"R(.*)C(.*)");

            var calls = puzzle.ToEnumerable((line) => line.ToArray()).ToArray();

            string[][] seats = new string[rows][].Select(i => i = new string[cols]).ToArray();

            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    seats[row][col] = $"R{row}C{col}";

            foreach (var instructions in calls)
            {
                var seat = ProcessSeatBSP(seats, instructions, 0);

                var match = pattern.Match(seat[0][0]);

                ids.Add(int.Parse(match.Groups[1].Value) * 8 + int.Parse(match.Groups[2].Value));
            }

            return ids.ToArray();
        }

        private string[][] ProcessSeatBSP(string[][] seats, char[] instructions, int position)
        {
            if (position < instructions.Length)
            {
                char instruction = instructions[position];

                if (position < instructions.Length - 3 && seats.Length != 1)
                {
                    int center = seats.Length / 2;

                    switch (instruction)
                    {
                        case 'F':
                            seats = seats[0..center];
                            break;
                        case 'B':
                            seats = seats[center..seats.Length];
                            break;
                    }
                }
                else
                {
                    int center = seats[0].Length / 2;

                    switch (instruction)
                    {
                        case 'L':
                            seats[0] = seats[0][0..center];
                            break;
                        case 'R':
                            seats[0] = seats[0][center..seats[0].Length];
                            break;
                    }
                }

                seats = ProcessSeatBSP(seats, instructions, position + 1);
            }

            return seats;
        }
    }
}