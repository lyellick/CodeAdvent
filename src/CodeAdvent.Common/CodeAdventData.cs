using RestSharp;

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
        public static async Task<string> GetData(int year, int day)
        {
            if (!TryGetEnvironmentVariable("AOCSession", out string token))
                throw new Exception("Missing environment variable: AOCSession.");

            if (!Uri.TryCreate($@"https://adventofcode.com/{year}/day/{day}/input", UriKind.RelativeOrAbsolute, out Uri uri))
                throw new UriFormatException();

            string cache = Path.Join(Path.GetTempPath(), $"AOC{year}{day:00}.txt");

            if (!File.Exists(cache))
            {
                using var client = new RestClient(uri);

                RestRequest request = new();

                request.AddHeader("Cookie", $"session={token}");

                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await File.WriteAllTextAsync(cache, response.Content);

                    return await File.ReadAllTextAsync(cache);
                }
            }
            else
            {
                return await File.ReadAllTextAsync(cache);
            }

            return null;
        }

        private static bool TryGetEnvironmentVariable(string key, out string value)
        {
            value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
            return !string.IsNullOrEmpty(value);
        }
    }
}
