using OnTime_Demo.IServices;
using System.Web;

namespace OnTime_Demo.Services
{
    public class JiraHttpClient : IJiraHttpClient
    {
        private readonly HttpClient _client;
        public JiraHttpClient(HttpClient client)
        {
            _client = client;
        }
        static string getqueryString(string url, Dictionary<object, object> parameters)
        {
            var ub = new UriBuilder(url);
            var param = HttpUtility.ParseQueryString(ub.Query);
            foreach (var parameter in parameters)
                param.Add(parameter.Key.ToString(), parameter.Value.ToString());
            ub.Query = param.ToString();
            return ub.ToString();
        }

        public async Task<HttpResponseMessage> getAsync(string url, Dictionary<object, object> parameters, string authToken)
        {
            //var client = _clientFactory.CreateClient("zohoClient");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", authToken);
            var responce = await _client.GetAsync(getqueryString(url, parameters));
            if (responce.IsSuccessStatusCode)
                return responce;
            else if (!responce.IsSuccessStatusCode && responce.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return responce;
            }
            else
                throw new Exception();

        }
    }
}
