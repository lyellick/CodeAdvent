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
            int rows = 128, cols = 8;

            var calls = _puzzle.ToEnumerable((line) => line.ToArray()).ToArray();

            int[][] seats = new int[rows][].Select(i => i = new int[cols]).ToArray();

            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    seats[row][col] = row;

            foreach (var instructions in calls)
            {
                ProcessSeatBSP(seats, instructions, 0);
            }

            Assert.Pass();
        }

        [Test]
        public void Part2()
        {
            Assert.Pass();
        }

        private (int row, int col) ProcessSeatBSP(int[][] seats, char[] instructions, int position)
        {
            var seat = (0, 0);

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
                    int center = seats[^1].Length / 2;

                    switch (instruction)
                    {
                        case 'L':
                            seats[^1] = seats[^1][0..center];
                            break;
                        case 'R':
                            seats[^1] = seats[^1][center..seats[^1].Length];
                            break;
                    }
                }

                seat = ProcessSeatBSP(seats, instructions, position + 1);
            }

            return seat;
        }
    }
}