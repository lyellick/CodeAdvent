using System.Text.Json;

namespace CodeAdvent.Event.Y2015.Puzzles
{
    /// <summary>
    /// Day 12: JSAbacusFramework.io
    /// </summary>
    public class Day12
    {
        private CodeAdventPuzzle _puzzle;

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2015, 12);

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            int sum = ProcessJson(_puzzle.Input);

            Assert.That(sum, Is.EqualTo(191164));
        }

        [Test]
        public void Part2()
        {
            int sum = ProcessJson(_puzzle.Input, "red");

            Assert.That(sum, Is.EqualTo(87842));
        }

        private int ProcessJson(string input, string exclude = "")
        {
            int walkJson(JsonElement t)
            {
                return t.ValueKind switch
                {
                    JsonValueKind.Object when !string.IsNullOrEmpty(exclude) && t.EnumerateObject().Any(p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == exclude) => 0,
                    JsonValueKind.Object => t.EnumerateObject().Select(p => walkJson(p.Value)).Sum(),
                    JsonValueKind.Array => t.EnumerateArray().Select(walkJson).Sum(),
                    JsonValueKind.Number => t.GetInt32(),
                    _ => 0
                };
            }

            return walkJson(JsonDocument.Parse(input).RootElement);
        }
    }
}