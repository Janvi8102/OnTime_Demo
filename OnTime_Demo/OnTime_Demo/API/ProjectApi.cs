using Newtonsoft.Json;
using OnTime_Demo.Entities;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;

namespace OnTime_Demo.API
{
    public class ProjectApi : ApiBase, IProjectApi
    {
        private readonly IJiraHttpClient _jiraclient;
        private readonly IUserServices _userServices;
        public ProjectApi(IJiraHttpClient jiraclient, IUserServices userServices)
        {
            _jiraclient = jiraclient;
            _userServices = userServices;
        }
        public Dictionary<object, object> getQueryParameters(Dictionary<object, object> queryParameters)
        {
            if (queryParameters == null)
                queryParameters = new Dictionary<object, object>();
            //queryParameters.Add("authtoken", authtoken);
            return queryParameters;
        }

        public async Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var data = new List<ProjectOutput>();
            string url = getBaseUrl() + "/rest/api/2/project";
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

        public async Task<CommentOutput> AddComment(string IssueKey, CommentInput content, JiraCommonInput jiraCommonInput)
        {
            var data = new CommentOutput();
            var requestBody = new Dictionary<object, object>();
            requestBody.Add("body", content.body);
            string url = getBaseUrl() + "/rest/api/2/issue/" + IssueKey + "/comment";
            var response = await _jiraclient.postAsync(url, getQueryParameters(requestBody), jiraCommonInput.AuthToken);
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<CommentOutput>(response.Content.ReadAsStringAsync().Result);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return data;
        }

        public async Task<IssueOutput> GetIssue(string IssueKey, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var demo = new IssueOutput();
            string url = getBaseUrl() + "/rest/api/2/issue/" + IssueKey ;
            var result = await _jiraclient.getAsync(url, jiraCommonInput.AuthToken);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                demo = JsonConvert.DeserializeObject<IssueOutput>(data);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return demo;
        }

        public async Task<AllIssue> GetAllIssue(string ProjectKey, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var demo = new AllIssue();
            string url = getBaseUrl() + "/rest/api/2/search?jql=project=" + ProjectKey;
            var result = await _jiraclient.getAsync(url, jiraCommonInput.AuthToken);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                demo = JsonConvert.DeserializeObject<AllIssue>(data);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return demo;
        }

        public async Task<MyIssueOutput> GetMyIssue(string AccountId, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var demo = new MyIssueOutput();
            string url = getBaseUrl() + "/rest/api/2/search?jql=assignee=" + AccountId;
            var result = await _jiraclient.getAsync(url, jiraCommonInput.AuthToken);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                demo = JsonConvert.DeserializeObject<MyIssueOutput>(data);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return demo;
        }

        public async Task<MyIssueOutput> GetMyProjectIssue(string ProjectKey, string AccountId, JiraTokenModel jiramodel, JiraCommonInput jiraCommonInput)
        {
            var demo = new MyIssueOutput();
            string url = getBaseUrl() + "/rest/api/2/search?jql=project=" + ProjectKey + "+and+assignee=" + AccountId;
            var result = await _jiraclient.getAsync(url, jiraCommonInput.AuthToken);
            if (result.IsSuccessStatusCode)
            {
                var data = await result.Content.ReadAsStringAsync();
                demo = JsonConvert.DeserializeObject<MyIssueOutput>(data);
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return demo;
        }
        public async Task<WorkLogOutput> AddWorkLog(string IssueKey, WorkLogInput content, JiraCommonInput jiraCommonInput)
        {
            var data = new WorkLogOutput();
            var requestBody = ConvertLogInputToDictionary(content);

            string url = getBaseUrl() + "/rest/api/2/issue/" + IssueKey + "/worklog";
            var response = await _jiraclient.postAsync(url, getQueryParameters(requestBody), jiraCommonInput.AuthToken);
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<WorkLogOutput>(response.Content.ReadAsStringAsync().Result);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return data;
        }

        public Dictionary<object, object> ConvertLogInputToDictionary(WorkLogInput content)
        {
            var requestBody = new Dictionary<object, object>();
            if (content.comment != null)
            {
                requestBody.Add("comment", content.comment);
            }    
            if (content.started != null)
            {
                requestBody.Add("started", content.started);
            }              
            if (content.timeSpentSeconds != null)
            {
                requestBody.Add("timeSpentSeconds", content.timeSpentSeconds);
            }
                
            return requestBody;
        }

    }
}
