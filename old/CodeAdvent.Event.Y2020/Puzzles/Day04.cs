using CodeAdvent.Common.Extensions;
using System;
using System.Text.RegularExpressions;

namespace CodeAdvent.Event.Y2020.Puzzles
{
    /// <summary>
    /// Day 4: Passport Processing
    /// </summary>
    public class Day04
    {
        private CodeAdventPuzzle _puzzle;

        private readonly Dictionary<string, string> _fields = new() { { "byr", "Birth Year" }, { "iyr", "Issue Year" }, { "eyr", "Expiration Year" }, { "hgt", "Height" }, { "hcl", "Hair Color" }, { "ecl", "Eye Color" }, { "pid", "Passport ID" }, { "cid", "Country ID" } };

        private readonly string[] _eye = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        [SetUp]
        public async Task Setup()
        {
            _puzzle = await CodeAdventData.GetPuzzle(2020, 4);

            _puzzle.Input = _puzzle.Input.Replace("\n", " ").Replace("  ", "\n");

            Assert.That(_puzzle.Input, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void Part1()
        {
            var passports = _puzzle.ToEnumerable(@"(\w+):([^\s]+)", (matches) => matches.ToDictionary(match => match.Groups[1].Value, match => match.Groups[2].Value)).ToArray();

            int valid = passports.Count(passport => passport.Count == _fields.Count || (passport.Count == _fields.Count - 1 && !passport.ContainsKey("cid")));

            Assert.That(valid, Is.EqualTo(228));
        }

        [Test]
        public void Part2()
        {
            var passports = _puzzle.ToEnumerable(@"(\w+):([^\s]+)", (matches) => matches.ToDictionary(match => match.Groups[1].Value, match => match.Groups[2].Value)).ToArray();

            int valid = 0;

            foreach (var passport in passports)
            {
                bool hasRequiredFields = passport.Count == _fields.Count || (passport.Count == _fields.Count - 1 && !passport.ContainsKey("cid"));

                if (hasRequiredFields)
                {
                    bool hasValidBirthYear = passport["byr"].Length == 4 && int.Parse(passport["byr"]) >= 1920 && int.Parse(passport["byr"]) <= 2002;
                    bool hasValidIssueYear = passport["iyr"].Length == 4 && int.Parse(passport["iyr"]) >= 2010 && int.Parse(passport["iyr"]) <= 2020;
                    bool hasValidExpirationYear = passport["eyr"].Length == 4 && int.Parse(passport["eyr"]) >= 2020 && int.Parse(passport["eyr"]) <= 2030;
                    bool hasValidHeight = false;
                    bool hasValidHairColor = Regex.IsMatch(passport["hcl"], @"#[0-9a-f]{6}");
                    bool hasValidEyeColor = _eye.Contains(passport["ecl"]);
                    bool hasValidPassportID = false;

                    if (passport["hgt"].Contains("cm") || passport["hgt"].Contains("in"))
                    {
                        int height = int.Parse(Regex.Match(passport["hgt"], @"\d+").Value);

                        hasValidHeight = passport["hgt"].Contains("cm") ? height >= 150 && height <= 193 : height >= 59 && height <= 76;
                    }

                    if (int.TryParse(passport["pid"], out int pid))
                        hasValidPassportID = passport["pid"].Length == 9;

                    if (hasValidBirthYear && hasValidIssueYear && hasValidExpirationYear && hasValidHeight && hasValidHairColor && hasValidEyeColor && hasValidPassportID)
                        valid++;

                }
            }

            Assert.That(valid, Is.EqualTo(175));
        }
    }
}