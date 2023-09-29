using System.Net.Http.Headers;

namespace Services.Helpers
{
    public static class HttpFactory
    {
        /// <summary>
        /// Extension method for setting required headers
        /// </summary>
        /// <param name="client">Instance of Http client</param>
        /// <param name="baseUrl">Request base url</param>
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
