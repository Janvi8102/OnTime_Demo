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
            var data = new ConfigurationInfoForZohoRefreshToken
            {
                client_id = configValues.client_id,
                client_secret = configValues.client_secret,
                grant_type = configValues.grant_type,
                refresh_token = "eyJraWQiOiI1MWE2YjE2MjRlMTQ5ZDFiYTdhM2VmZjciLCJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiI2MmViZGQzMjQzMmVmNDk0YzhjYTY0OGIiLCJuYmYiOjE2NjM1ODYzNTQsImlzcyI6Imh0dHBzOi8vYXRsYXNzaWFuLWFjY291bnQtcHJvZC5wdXMyLmF1dGgwLmNvbS8iLCJpYXQiOjE2NjM1ODYzNTQsImV4cCI6MTY5NTE0MzMwNiwiYXVkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJqdGkiOiI4NzA2NDcwMS03ZmQwLTQ3MmMtYWRmNC02NTRlODY1NjcxYWYiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vcmVmcmVzaF9jaGFpbl9pZCI6IjlGYjBnNUFKR3Jhb21ubU1uS1VPWXRISFJpalJiUTliLTYyZWJkZDMyNDMyZWY0OTRjOGNhNjQ4Yi03YjRiNzEyZi0yNWI1LTRmMjYtYmJhNC1iNTFjNzMzZTQ2YzIiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vdmVyaWZpZWQiOnRydWUsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS91anQiOiJmMDZiMGQ5Mi0yMzliLTQ4YjQtODA2OS03YjZiNTRiYzRiN2EiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vYXRsX3Rva2VuX3R5cGUiOiJST1RBVElOR19SRUZSRVNIIiwic2NvcGUiOiJtYW5hZ2U6amlyYS1jb25maWd1cmF0aW9uIG1hbmFnZTpqaXJhLWRhdGEtcHJvdmlkZXIgbWFuYWdlOmppcmEtcHJvamVjdCBtYW5hZ2U6amlyYS13ZWJob29rIG9mZmxpbmVfYWNjZXNzIHJlYWQ6amlyYS11c2VyIHJlYWQ6amlyYS13b3JrIHdyaXRlOmppcmEtd29yayIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS9wYXJlbnRfYWNjZXNzX3Rva2VuX2lkIjoiMWNiYTBlNjYtM2NhOC00Zjk0LTkxZWYtM2I3NmNlZmY2NzViIiwidmVyaWZpZWQiOiJ0cnVlIiwiaHR0cHM6Ly9pZC5hdGxhc3NpYW4uY29tL3Nlc3Npb25faWQiOiJkNjc5ODg4MS04ZDYwLTQ4NzEtOWQzMS00NDE1ZWRlZWQ5MDAifQ.rgiO0l3D6TVWWRHTh6MVfJ1FzjtOZjtyBpmIsDmG8V6qg-DpGb1j5Zoep-U6GyY0LpnkuUG1lgs0zftXy8Tbp4BweD8uBWEILYvgAsq8VxW_TCw2qrlDL9RSzxcYNlG5X8PoCeSyteuEW_RnesrI_Fv84rXjldbr2LlmyGE9P-rqB6DBECUZv23DXKzNbfploF7qWY5seJIi-8rf05nfO3HVUDpWef9M8MHFQ1BSfbq8Sy_jXeUzHLuhyqplr_K1GQfBw1q__BTzWdexmAnhGz-JOYuV69NkhzFqWAl2DCiz15jDt4LXaebxKN8x2wQ2y7zSyzIweYV9WaWLRhx6bw"
            };

            var tokendata = System.Text.Json.JsonSerializer.Serialize(data) ;
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
                }
                else
                {
                    throw new Exception();
                }
                return response;
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
