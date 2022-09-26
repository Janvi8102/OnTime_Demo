using OnTime_Demo.IServices;
using System.Text;

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

        public async Task<HttpResponseMessage> postAsync(string url, Dictionary<object, object> requestBody, string authToken)
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", authToken);

            var requestdata = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestdata, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(url);
            var responce = await client.PostAsync(client.BaseAddress.ToString(), requestContent);
            if (responce.IsSuccessStatusCode)
                return responce;
            else if (!responce.IsSuccessStatusCode && responce.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return responce;
            }
            else
                return responce;


        }
    }
}
