using Newtonsoft.Json;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;



namespace OnTime_Demo.Api
{
    public class ProjectApi : IProjectApi
    {
        
        public IJiraHttpClient _JiraHttpClient { get; }
        public ProjectApi(IJiraHttpClient jiraHttpClient)
        {
            _JiraHttpClient = jiraHttpClient;
        }
        public Dictionary<object, object> getQueryParameters()
        {
            var queryParameters = new Dictionary<object, object>();
            //queryParameters.Add("authtoken", authtoken);
            return queryParameters;
        }

        public async Task<List<ProjectOutput>> RecentProject()
        {
            var authtoken = "Bearer Y0OGIiLCJuYmYiOjE2NjMzMDIxMzIsImlzcyI6Imh0dHBzOi8vYXRsYXNzaWFuLWFjY291bnQtcHJvZC5wdXMyLmF1dGgwLmNvbS8iLCJpYXQiOjE2NjMzMDIxMzIsImV4cCI6MTY2MzMwNTczMiwiYXVkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJqdGkiOiJhYTEyMjNhNC0yNmJlLTRiMzAtOGRkNS02Zjk2ZGZlMjYzZGEiLCJodHRwczovL2F0bGFzc2lhbi5jb20vdmVyaWZpZWQiOnRydWUsInZlcmlmaWVkIjoidHJ1ZSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS9zZXNzaW9uX2lkIjoiZDY3OTg4ODEtOGQ2MC00ODcxLTlkMzEtNDQxNWVkZWVkOTAwIiwiY2xpZW50X2lkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vcmVmcmVzaF9jaGFpbl9pZCI6IjlGYjBnNUFKR3Jhb21ubU1uS1VPWXRISFJpalJiUTliLTYyZWJkZDMyNDMyZWY0OTRjOGNhNjQ4Yi1jNzI1YTEwNC1lYmNmLTRmODktOGFkNS1mNTdiYzBkYmQ3NGIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vc3lzdGVtQWNjb3VudEVtYWlsIjoiMWMwNmY5NWYtODlkMy00MzJlLTg1OGMtODFlNjI1ZmZkOTYxQGNvbm5lY3QuYXRsYXNzaWFuLmNvbSIsImh0dHBzOi8vaWQuYXRsYXNzaWFuLmNvbS91anQiOiJjMjgxMjIzOS04NDMzLTQyN2ItYTgwMC01NTBiMjJhMmNjOGYiLCJodHRwczovL2lkLmF0bGFzc2lhbi5jb20vdmVyaWZpZWQiOiJ0cnVlIiwiaHR0cHM6Ly9pZC5hdGxhc3NpYW4uY29tL2F0bF90b2tlbl90eXBlIjoiQUNDRVNTIiwic2NvcGUiOiJtYW5hZ2U6amlyYS1jb25maWd1cmF0aW9uIG1hbmFnZTpqaXJhLWRhdGEtcHJvdmlkZXIgbWFuYWdlOmppcmEtcHJvamVjdCBtYW5hZ2U6amlyYS13ZWJob29rIG9mZmxpbmVfYWNjZXNzIHJlYWQ6amlyYS11c2VyIHJlYWQ6amlyYS13b3JrIHdyaXRlOmppcmEtd29yayIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS8zbG8iOnRydWUsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9vYXV0aENsaWVudElkIjoiOUZiMGc1QUpHcmFvbW5tTW5LVU9ZdEhIUmlqUmJROWIiLCJodHRwczovL2F0bGFzc2lhbi5jb20vZW1haWxEb21haW4iOiJzaW1mb3Jtc29sdXRpb25zLmNvbSIsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50RW1haWxEb21haW4iOiJjb25uZWN0LmF0bGFzc2lhbi5jb20iLCJodHRwczovL2F0bGFzc2lhbi5jb20vZmlyc3RQYXJ0eSI6ZmFsc2UsImh0dHBzOi8vYXRsYXNzaWFuLmNvbS9zeXN0ZW1BY2NvdW50SWQiOiI2MzFiMTNhMzY4NTZiZGQ2MGFhMGQ5ZmQifQ.q3RcusQPeQD2YbMk6UoGISK8beSuGDbiQzPuzdtR6Eci2bzPJjeIGHqRA1rwGWUXpn5UJAC2TBhOOaeuW2DvgCQFX4jSf1JIvH_EYwzNsXLsXQZWYJsVcLDs227E_-jToLVmEZQmzgHsWp4_QhE6aKlhtbMD242f7UG0ixlI4WDEMBe0xQ_qcQUU9nhEkjA3T9k7jhafzEtFV8y56q3_vznp4Afc8lWg41iz4gxN2YEWGzlnIdB6FJOuocUaPWPt9tfwnvAOOZx56Uwo5WzuTvNFYuHOp3svHMY8_mTrgSVR19P_2IruT0Vd21QocyUcy7IF_lEfQ6x7-1qQvEyuAA";
            string url = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45/rest/api/3/project";
            var response = await _JiraHttpClient.getAsync(url, getQueryParameters(), authtoken);
           
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                List<ProjectOutput> taskList = new List<ProjectOutput>();
                return taskList;
            }
            else
            {
               return (await response.Content.ReadAsAsync<List<ProjectOutput>>());
              
            }

        }
    }
}
