using NUnit.Framework;
using CodeAdvent.Shared;

namespace CodeAdvent.Tests;

/// <summary>
/// </summary>
[Ignore("template unit test does not need to be ran")]
public class Template
{
    private readonly int _year = 0;
    private readonly int _day = 0;
    private string _session;
    private string _input;

    [SetUp]
    public async Task Setup()
    {
        _session = File.ReadAllText(Path.Join(Path.GetTempPath(), $"AOCSession.txt"));

        Assert.That(_session, Is.Not.Null.Or.Empty);

        _input = await CodeAdventData.GetPuzzelInputAsync(_session, _year, _day);

        Assert.That(_input, Is.Not.Null.Or.Empty);
    }

    [Test]
    public async Task Part1()
    {
        bool isCorrect = await CodeAdventData.SubmitPuzzelAnswerAsync(_session, _year, _year, 1, string.Empty);

        Assert.That(isCorrect, Is.True);
    }

    [Test]
    public async Task Part2()
    {
        bool isCorrect = await CodeAdventData.SubmitPuzzelAnswerAsync(_session, _year, _year, 2, string.Empty);

        Assert.That(isCorrect, Is.True);
    }
}