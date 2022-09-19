using OnTime_Demo.IServices;

namespace OnTime_Demo.Services
{
    public class JiraHttpClient : IJiraHttpClient
    {
        private readonly HttpClient client;
        public JiraHttpClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<HttpResponseMessage> getAsync(string url, string authToken)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", authToken);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else if (!response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return response;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
