using Dapper;
using Newtonsoft.Json;
using Npgsql;
using OnTime_Demo.Entities;
using OnTime_Demo.IRepository;
using OnTime_Demo.IServices;
using System.Data;
using System.Text;
using System.Text.Json;

namespace OnTime_Demo.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository userRepository;
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public JiraTokenModel GetJiraTokens(int userId)
        {
            return userRepository.GetJiraToken(userId);
        }

        public async Task<JiraAccountModel> JiraAccountRefreshToken(string refreshUrl)
        {
            var response = new JiraAccountModel();
            List<ConfigurationInfoModel> configurationInfo = userRepository.GetConfigurationInfo();
            ConfigurationInfoForZohoRefreshToken configValues = new ConfigurationInfoForZohoRefreshToken();
            JiraRefreshToken refreshToken = userRepository.GetJiraRefreshToken();

            foreach (var info in configurationInfo)
            {
                if (info.key.Equals("clientId"))
                {
                    configValues.client_id = info.value;
                }
                if (info.key.Equals("clientSecret"))
                {
                    configValues.client_secret = info.value;
                }
                if (info.key.Equals("grantTypeRefresh"))
                {
                    configValues.grant_type = info.value;
                }
            }

            var requestBody = new Dictionary<object, object>();
            if (configValues.client_id != null)
            {
                requestBody.Add("client_id", configValues.client_id);
            }
            if (configValues.client_secret != null)
            {
                requestBody.Add("client_secret", configValues.client_secret);
            }
            if (configValues.grant_type != null)
            {
                requestBody.Add("grant_type", configValues.grant_type);
            }
            if (refreshToken.refresh_token != null)
            {
                requestBody.Add("refresh_token", refreshToken.refresh_token);
            }

            var tokendata = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(tokendata, Encoding.UTF8, "application/json");
            string url = refreshUrl;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            HttpResponseMessage apiResult = await client.PostAsync(client.BaseAddress.ToString(), requestContent);
            try
            {
                if (apiResult.IsSuccessStatusCode)
                {
                    var dataObjects = await apiResult.Content.ReadAsStringAsync();
                    var finalAuthResponses = JsonConvert.DeserializeObject<JiraAccountModel>(dataObjects);
                    response = finalAuthResponses;
                    return response;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (System.Exception ex)
            {
                return response;
            }

        }

        public async Task<JiraTokenModel> RefreshingJiraToken(JiraTokenModel jiramodel)
        {          
            string refreshUrl = "https://auth.atlassian.com/oauth/token";
            JiraAccountModel jiraResponseToken = await JiraAccountRefreshToken(refreshUrl);
            JiraTokenModel jiratokens = new JiraTokenModel();
            jiratokens.JiraAuthToken = jiraResponseToken.access_token;
            jiratokens.JiraRefreshToken = jiraResponseToken.refresh_token;
            jiratokens.UserId = jiramodel.UserId;
            if(jiratokens.JiraAuthToken == null || jiratokens.JiraRefreshToken == null)
            {
                throw new Exception("Refresh Token is invalid");
            }
            else
            {
                bool updateStatus = await userRepository.UpdateJiraTokens(jiratokens);
                if (updateStatus)
                {
                    return jiratokens;
                }
                else
                {
                    JiraTokenModel emptyZohoToken = new JiraTokenModel();
                    return emptyZohoToken;
                }
            }
            
        }

       
    }
}
