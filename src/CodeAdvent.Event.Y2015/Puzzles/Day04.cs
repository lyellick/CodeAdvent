using System.Text;
using System.Security.Cryptography;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 4: The Ideal Stocking Stuffer
    /// </summary>
    public class Day04
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2015, 4);
            _puzzle.Input = _puzzle.Input.TrimEnd('\n');

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var result = Mine(_puzzle.Input, "00000");

            Assert.That(result.secret, Is.EqualTo(282749));
        }

        [Test]
        public void Part2()
        {
            var result = Mine(_puzzle.Input, "000000");

            Assert.That(result.secret, Is.EqualTo(9962624));
        }

        private (string hash, int secret, string output) Mine(string input, string startsWith)
        {
            int secret = 0;
            string hash = "";

            do
            {
                secret++;
                hash = CreateMD5($"{input}{secret}");
            } 
            while (!hash.StartsWith(startsWith));

            return (hash, secret, $"{input}{secret}");
        }

        private string CreateMD5(string input)
        {
            using MD5 md5 = MD5.Create();
            
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            
            byte[] hash = md5.ComputeHash(bytes);

            return Convert.ToHexString(hash);
        }
    }
}