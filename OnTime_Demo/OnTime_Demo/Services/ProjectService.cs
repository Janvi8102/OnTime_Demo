using Newtonsoft.Json;
using OnTime_Demo.API;
using OnTime_Demo.Entities;
using OnTime_Demo.IServices;
using OnTime_Demo.Models;

namespace OnTime_Demo.Services
{
    public class ProjectService : IProjectService
    {
        JiraCommonInput jiraCommonInput;
        private readonly IProjectApi _projectApi;
        private readonly IUserServices _userServices;
        
        public ProjectService(IProjectApi projectApi, IUserServices userServices)
        {
           _projectApi = projectApi;
            _userServices = userServices;
        }

        public void setAuthorizationToken(string authToken)
        {
            jiraCommonInput = new JiraCommonInput(authToken);
        }
        public async Task<List<ProjectOutput>> MyProject(JiraTokenModel jiramodel)
        {
           List<ProjectOutput> myProject = await _projectApi.MyProject(jiramodel, jiraCommonInput);
           if(myProject == null)
            {
                JiraTokenModel jiratokens = await _userServices.RefreshingJiraToken(jiramodel);
                if (!string.IsNullOrEmpty(jiratokens.JiraAuthToken))
                {
                    setAuthorizationToken("Bearer " + jiratokens.JiraAuthToken);
                }
                myProject = await _projectApi.MyProject(jiramodel, jiraCommonInput);
            }
            return myProject;
        }
    }
}
