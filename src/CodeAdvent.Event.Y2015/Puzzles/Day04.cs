using System.Text;
using System.Security.Cryptography;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 4: The Ideal Stocking Stuffer
    /// </summary>
    public class Day04
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetData(2015, 4);
            _input = _input.TrimEnd('\n');

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var result = Mine(_input, "00000");

            Assert.That(result.secret, Is.EqualTo(282749));
        }

        [Test]
        public void Part2()
        {
            var result = Mine(_input, "000000");

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