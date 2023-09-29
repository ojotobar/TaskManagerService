using System.Net.Http.Headers;

namespace Services.Helpers
{
    public static class HttpFactory
    {
        public static void SetHeaders(this HttpClient client, string baseUrl)
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(baseUrl);
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
