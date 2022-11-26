using RestSharp;
using CodeAdvent.Common.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common
{
    public static class CodeAdventData
    {
        /// <summary>
        /// Get the advent of code's input data based off the year and day. 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UriFormatException"></exception>
        public static async Task<CodeAdventEvent> GetEvent(int year, int day)
        {
            CodeAdventEvent codeAdventEvent = new();

            Regex pattern = new(@"<h2>--- Day \d: (.*) ---<\/h2>");

            if (!TryGetEnvironmentVariable("AOCSession", out string token))
                throw new Exception("Missing environment variable: AOCSession.");

            if (!Uri.TryCreate($@"https://adventofcode.com/{year}", UriKind.RelativeOrAbsolute, out Uri calendarUri))
                throw new UriFormatException();

            if (!Uri.TryCreate($@"https://adventofcode.com/{year}/day/{day}/input", UriKind.RelativeOrAbsolute, out Uri puzzleInputUri))
                throw new UriFormatException();

            if (!Uri.TryCreate($@"https://adventofcode.com/{year}/day/{day}", UriKind.RelativeOrAbsolute, out Uri puzzlePageUri))
                throw new UriFormatException();

            string cache = Path.Join(Path.GetTempPath(), $"AOC{year}{day:00}.txt");

            if (!File.Exists(cache))
            {
                using var puzzleInputClient = new RestClient(puzzleInputUri);

                RestRequest request = new();

                request.AddHeader("Cookie", $"session={token}");

                RestResponse response = await puzzleInputClient.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await File.WriteAllTextAsync(cache, response.Content);

                    string input = await File.ReadAllTextAsync(cache);

                    codeAdventEvent = new() { Year = year, Day = day, Input = input };
                }
            }
            else
            {
                string input = await File.ReadAllTextAsync(cache);

                codeAdventEvent = new() { Year = year, Day = day, Input = input };
            }

            using HttpClient puzzlePageClient = new();

            string puzzlePage = await puzzlePageClient.GetStringAsync(puzzlePageUri);

            if (!string.IsNullOrEmpty(puzzlePage))
            {
                var match = pattern.Match(puzzlePage);
                codeAdventEvent.Title = match.Groups[1].Value;
                codeAdventEvent.Calendar = calendarUri;
                codeAdventEvent.Puzzle = puzzlePageUri;
            }

            return codeAdventEvent;
        }

        private static bool TryGetEnvironmentVariable(string key, out string value)
        {
            value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
            return !string.IsNullOrEmpty(value);
        }
    }
}
