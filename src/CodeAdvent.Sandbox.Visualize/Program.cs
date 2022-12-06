using CodeAdvent.Common;
using CodeAdvent.Common.Extensions;
using System.Runtime.ExceptionServices;

var puzzle = await CodeAdventData.GetPuzzle(2022, 5);

var stacks = puzzle.ToEnumerable(
    @"(.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3}) (.{3})",
    (match) => match.Groups.Values.Select(val => val.Value.Replace("[", "").Replace("]", "")).Skip(1).ToList())
    .Take(8).ToList().Pivot();

var instructions = puzzle.ToEnumerable<(int take, int from, int to)>(
    @"move (.*) from (.*) to (.*)",
    (match, isMatch) => isMatch
        ? (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value))
        : (0, 0, 0))
    .Skip(10).ToArray();

Print(stacks);

Thread.Sleep(1000);

// Day 5 Part 1 Visualization
foreach (var (take, from, to) in instructions)
{
    var collection = stacks[from - 1].Take(take).ToArray();

    stacks[from - 1] = stacks[from - 1].Skip(take).ToList();

    foreach (var item in collection)
        stacks[to - 1] = stacks[to - 1].Prepend(item).ToList();

    Print(stacks);

    Thread.Sleep(1000);
}

void Print(List<List<string>> jarray)
{
    Console.Clear();

    var output = AddSpace(jarray);

    for (int row = 0; row < output[0].Count; row++)
    {
        for (int col = 0; col < output.Count; col++)
        {
            var cell = output[col][row];
            if (!string.IsNullOrWhiteSpace(cell))
            {
                Console.Write($"[{cell}]");
            }
            else
            {
                Console.Write($"[ ]");
            }
        }
        Console.Write("\n\r");
    }
}

// TODO: the issue here is this method is not taking index compred to row length into account.
// Basically, if the row is not equal to max, but inbetween each actual cell is a space then make sure it represents that
// [ ][ ][S][M] is actually [ ][S][ ][M]
static List<List<string>> AddSpace(List<List<string>> jarray)
{
    var max = jarray.Max(col => col.Count);

    for (int stack = 0; stack < jarray.Count; stack++)
        if (jarray[stack].Count != max)
            foreach (var space in Enumerable.Repeat("  ", max - jarray[stack].Count).ToList())
                jarray[stack] = jarray[stack].Prepend(space).ToList();

    return jarray;
}