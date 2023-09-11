using System.Net;

namespace CodeAdvent.Shared;

public static class CodeAdventData
{
    private static readonly Uri _url = new("https://adventofcode.com/");

    public static async Task<string> GetPuzzelInputAsync(string session, int year, int day)
    {
        string cache = Path.Join(Path.GetTempPath(), $"AOC{year}{day:00}.txt");

        if (!File.Exists(cache))
        {
            CookieContainer cookie = new();

            using HttpClientHandler handler = new() { CookieContainer = cookie };
            using HttpClient client = new(handler) { BaseAddress = _url };

            cookie.Add(_url, new Cookie("session", session));

            HttpResponseMessage response = await client.GetAsync($"{year}/day/{day}/input");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Unable to get puzzle input.");

            string input = await response.Content.ReadAsStringAsync();

            return input;
        }
        else
        {
            string input = await File.ReadAllTextAsync(cache);

            return input;
        }
    }

    public static async Task<bool> SubmitPuzzelAnswerAsync(string session, int year, int day, int part, string answer)
    {
        throw new NotImplementedException();
    }
}
