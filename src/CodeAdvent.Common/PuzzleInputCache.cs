using RestSharp;

namespace CodeAdvent.Common
{
    public static class PuzzleInputCache
    {
        public static async Task<string> GetInput(string url)
        {
            string session = "session=53616c7465645f5f113943f33702368799e2d3cf848a2ce8f142e904fee30d69b122dcaf070fc368f93ae283c5251f339c46c8664e22d52e86987bac82bf64ad";

            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uri))
                throw new UriFormatException();

            string[] parts = uri.LocalPath.Split('/');

            string year = parts[1];
            string day = parts[3];

            string path = Path.Join(Path.GetTempPath(), $"AOC_{year}-{day}.txt");

            if (File.Exists(path))
            {
                return await File.ReadAllTextAsync(path);
            }
            else
            {
                using var client = new RestClient(uri);

                RestRequest request = new();

                request.AddHeader("Cookie", session);

                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await File.WriteAllTextAsync(path, response.Content);
                    
                    return await File.ReadAllTextAsync(path);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
