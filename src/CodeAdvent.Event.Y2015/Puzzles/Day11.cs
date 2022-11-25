using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 11: Corporate Policy
    /// </summary>
    public class Day11
    {
        private string _input;

        [SetUp]
        public async Task Setup()
        {
            _input = await CodeAdventData.GetInput(2015, 11);
            _input = _input.TrimEnd('\n');

            Assert.That(_input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            string password = ProcessNextPassword(_input);

            Assert.That(password, Is.EqualTo("hxbxxyzz"));
        }

        [Test]
        public void Part2()
        {
            string password = ProcessNextPassword(_input);

            password = ProcessNextPassword(password);

            Assert.That(password, Is.EqualTo("hxcaabcc"));
        }

        private string ProcessNextPassword(string input)
        {
            Regex findDoubles = new(@"(.)\1");

            int iteration = 0;
            int inputIndex = input.Length - 1;
            bool incrementNextIndex = false;
            bool run = true;

            do
            {
                var output = IncreaseLetterAtIndex(input, inputIndex);

                incrementNextIndex = output.next == 'a';

                if (incrementNextIndex)
                {
                    inputIndex = inputIndex == 0 ? input.Length - 1 : inputIndex - 1;
                }
                else
                {
                    inputIndex = input.Length - 1;
                }

                input = output.input;
                iteration++;

                bool hasIncreasingStraight = AlphabetSplit().Any(split => input.Contains(split));
                bool hasIllegalLetters = new string[] { "i", "o", "l" }.Any(illegal => input.Contains(illegal));
                bool hasMoreThanOneDouble = findDoubles.Matches(input).Count > 1;

                if (hasIncreasingStraight && !hasIllegalLetters && hasMoreThanOneDouble)
                    run = false;

            } while (run);

            return input;
        }

        private static string ChangeCharacterByIndex(string input, int index, char next)
        {
            char[] array = input.ToCharArray();
            array[index] = next;
            input = new string(array);
            return input;
        }

        private char NextLetter(char current)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            return alphabet.IndexOf(current) != alphabet.Length - 1 ? alphabet[alphabet.IndexOf(current) + 1] : alphabet[0];
        }

        private string[] AlphabetSplit()
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            List<string> parts = new();

            for (int i = 0; i < alphabet.Length; i++)
                if (i < alphabet.Length - 2)
                    parts.Add(string.Join("", alphabet[i], alphabet[i + 1], alphabet[i + 2]));

            return parts.ToArray();
        }

        private (string input, char next) IncreaseLetterAtIndex(string input, int index)
        {
            char n = input[index];

            char next = NextLetter(n);

            return (ChangeCharacterByIndex(input, index, next), next);
        }
    }
}