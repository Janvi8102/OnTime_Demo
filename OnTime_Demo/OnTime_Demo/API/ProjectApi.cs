using Newtonsoft.Json;
using OnTime_Demo.Entities;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;

namespace OnTime_Demo.API
{
    public class ProjectApi : IProjectApi
    {
        private readonly IJiraHttpClient _jiraclient;
        private readonly IUserServices _userServices;
        public ProjectApi(IJiraHttpClient jiraclient, IUserServices userServices)
        {
            _jiraclient = jiraclient;
            _userServices = userServices;
        }
        public async Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var data = new List<ProjectOutput>();
            string url = "https://api.atlassian.com/ex/jira/c69523f7-cd4d-40d6-bd88-a2b386222c45/rest/api/3/project";
            var result = await _jiraclient.getAsync(url, jiraCommonInput.AuthToken);
            if (result.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<List<ProjectOutput>>(result.Content.ReadAsStringAsync().Result);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return data;
        }
    }
}
